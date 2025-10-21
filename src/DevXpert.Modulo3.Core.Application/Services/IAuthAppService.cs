using DevXpert.Modulo3.Core.Application.ViewModels;

namespace DevXpert.Modulo3.Core.Application.Services;

public interface IAuthAppService
{
    Task<AuthResultViewModel> Cadastrar(CadastroViewModel cadastro);
    Task<AuthResultViewModel> Autenticar(LoginViewModel login);
}
