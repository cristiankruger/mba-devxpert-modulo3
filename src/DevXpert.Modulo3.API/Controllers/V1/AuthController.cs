using Asp.Versioning;
using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Core.Application.Services;
using DevXpert.Modulo3.Core.Application.ViewModels;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevXpert.Modulo3.API.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
[AllowAnonymous]
public class AuthController(IAuthAppService authAppService,
                            IAppIdentityUser user,
                            IMediatrHandler mediatrHandler,
                            INotificationHandler<DomainNotification> notifications) : MainController(user, mediatrHandler, notifications)
{
    [HttpPost]
    public async Task<IActionResult> Autenticar([FromBody] LoginViewModel login)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await authAppService.Autenticar(login);

        if (result.Sucesso)
            return CustomResponse(HttpStatusCode.OK, result.Token);

        foreach (var error in result.Erros)
            NotificarErro("Erro", error);

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
            NotificarErro("Erro", error);

        return CustomResponse(HttpStatusCode.BadRequest);
    }

}
