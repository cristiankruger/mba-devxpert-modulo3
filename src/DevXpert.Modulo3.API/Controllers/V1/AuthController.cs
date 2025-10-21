using Asp.Versioning;
using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Core.Application.Services;
using DevXpert.Modulo3.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevXpert.Modulo3.API.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class AuthController(IAuthAppService authAppService,
                            IAppIdentityUser user) : MainController(user)
{
    [HttpPost]
    public async Task<IActionResult> Autenticar([FromBody] LoginViewModel login)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await authAppService.Autenticar(login);

        if (result.Sucesso)
            return CustomResponse(HttpStatusCode.OK, result.Token);

        foreach (var error in result.Erros)
            NotificarErro(error);

        return CustomResponse(HttpStatusCode.BadRequest);
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroViewModel cadastro)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await authAppService.Cadastrar(cadastro);

        if (result.Sucesso)
            return CustomResponse(HttpStatusCode.OK, result.Token);

        foreach (var error in result.Erros)
            NotificarErro(error);

        return CustomResponse(HttpStatusCode.BadRequest);
    }

}
