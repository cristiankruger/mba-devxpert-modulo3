using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.API.Configurations.Extensions;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevXpert.Modulo3.API.Controllers;

[ApiController]
[AllowSynchronousIO]
[Authorize(AuthenticationSchemes = "Bearer")]
public abstract class MainController : ControllerBase
{
    private readonly IMediatrHandler _mediatorHandler;
    private readonly DomainNotificationHandler _notifications;
    protected Guid UserId { get; set; }
    protected string UserName { get; set; }
    protected string UserRole { get; set; }

    protected MainController(IAppIdentityUser user,
                             IMediatrHandler mediatorHandler,
                             INotificationHandler<DomainNotification> notifications)
    {
        _mediatorHandler = mediatorHandler;
        _notifications = (DomainNotificationHandler)notifications;

        if (user.IsAuthenticated())
        {
            UserId = user.GetUserId();
            UserName = user.GetUsername();
            UserRole = user.GetUserRole();
        }
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
            NotificarInvalidModelStateError(modelState);

        return CustomResponse();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (!_notifications.TemNotificacao())
            return Ok(new { success = true, data = result });

        return BadRequest(new { success = false, errors = _notifications.ObterNotificacoes().Select(n => n.Value) });        
    }

    protected void NotificarInvalidModelStateError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var erro in errors)
        {
            var errorMsg = erro.Exception is null ? erro.ErrorMessage : erro.Exception.Message;

            NotificarErro("Erro", errorMsg);
        }
    }

    protected void NotificarInvalidModelStateError(IdentityResult result)
    {
        foreach (var error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);

        NotificarInvalidModelStateError(ModelState);
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
        _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }

    private Guid GetObjectId(object result)
    {
        dynamic d = result;
        return (Guid)d.Id;
    }

    private object NotificarErros(object result)
    {
        return _notifications.TemNotificacao() ? _notifications.ObterNotificacoes().Select(n => n.Value) : result;
    }
}
