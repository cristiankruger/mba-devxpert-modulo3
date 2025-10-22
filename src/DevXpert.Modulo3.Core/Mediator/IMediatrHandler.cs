using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.Core.Messages;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;

namespace DevXpert.Modulo3.Core.Mediator;

public interface IMediatrHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
    Task<bool> EnviarComando<T>(T comando) where T : Command;
    Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
}
