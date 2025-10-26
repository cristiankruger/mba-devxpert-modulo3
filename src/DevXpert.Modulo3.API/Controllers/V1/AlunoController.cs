using Asp.Versioning;
using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Modulo3.API.Controllers.V1;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class AlunoController(IAppIdentityUser user,
                             IMediatrHandler mediatrHandler,
                             INotificationHandler<DomainNotification> notifications)
    : MainController(user, mediatrHandler, notifications)
{


}

