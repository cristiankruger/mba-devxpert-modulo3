using DevXpert.Modulo3.Core.Application.Extensions;
using DevXpert.Modulo3.Core.Application.ViewModels;
using DevXpert.Modulo3.Core.Application.ViewModels.Settings;
using DevXpert.Modulo3.Core.Domain;
using DevXpert.Modulo3.Core.Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevXpert.Modulo3.Core.Application.Services;

public class AuthAppService(IOptions<JwtSettings> JwtSettings,
                            UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager,
                            IAdminRepository adminRepository,
                            IAlunoRepository alunoRepository) : IAuthAppService
{
    public async Task<AuthResultViewModel> Autenticar(LoginViewModel login)
    {
        if (!await UsuarioExiste(login.Email))
            return new AuthResultViewModel(["Usuário ou senha incorretos."]);

        var result = await signInManager.PasswordSignInAsync(login.Email, login.Senha, false, true);

        if (!result.Succeeded)
            return new AuthResultViewModel(["Usuário ou senha incorretos."]);

        return await GerarToken(login.Email);
    }

    public async Task<AuthResultViewModel> Cadastrar(CadastroViewModel cadastro)
    {
        var user = new IdentityUser
        {
            UserName = cadastro.Email,
            Email = cadastro.Email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, cadastro.Senha);

        if (!result.Succeeded)
            return new AuthResultViewModel([.. result.Errors.Select(e => e.Description)]);

        var role = cadastro.EhAluno ? "Aluno" : "Admin";

        bool cadastrado = await HandleUsuario(user, role);

        if (!cadastrado)
            return new AuthResultViewModel([$"Falha ao cadastrar {role}."]);

        await signInManager.SignInAsync(user, false);

        return await GerarToken(user.Email);
    }

    private async Task<AuthResultViewModel> GerarToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var date = DateTime.Now;
        var claims = await BuscarClaims(email);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = JwtSettings.Value.Emissor,
            Claims = new Dictionary<string, object> { { JwtRegisteredClaimNames.Aud, JwtSettings.Value.ValidoEm } },
            Expires = date.AddMinutes(JwtSettings.Value.ExpiracaoTokenMinutos),
            NotBefore = date,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.Value.Jwt)),
                SecurityAlgorithms.HmacSha256Signature)
        });

        return new AuthResultViewModel(tokenHandler.WriteToken(token));
    }

    private async Task<bool> UsuarioExiste(string email)
    {
        var claims = await BuscarClaims(email);

        if (claims.PossuiRole("Admin"))
            return (await adminRepository.Obter(email)) is not null;

        if (claims.PossuiRole("Aluno"))
            return (await alunoRepository.Obter(email)) is not null;

        return false;
    }

    private async Task<List<Claim>> BuscarClaims(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null) return [];

        var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id)
            };

        foreach (var role in await userManager.GetRolesAsync(user))
            claims.Add(new(ClaimTypes.Role, role));

        return claims;
    }

    private async Task<bool> HandleUsuario(IdentityUser user, string role)
    {
        await userManager.AddToRoleAsync(user, role);

        if (role == "Admin")
        {
            var admin = new Admin(Guid.Parse(user.Id), user.UserName, user.Email);

            if (await adminRepository.Adicionar(admin))
            {
                await adminRepository.UnitOfWork.Commit();
                return true;
            }

            await userManager.DeleteAsync(user);
            return false;
        }

        if (role == "Aluno")
        {
            var aluno = new Aluno(Guid.Parse(user.Id), user.UserName, user.Email);

            if (await alunoRepository.Adicionar(aluno))
            {
                await alunoRepository.UnitOfWork.Commit();
                return true;
            }

            await userManager.DeleteAsync(user);
            return false;
        }

        return false;
    }

}
