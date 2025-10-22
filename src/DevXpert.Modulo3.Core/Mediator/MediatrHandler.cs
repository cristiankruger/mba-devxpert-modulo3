using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.Core.Messages;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using MediatR;

namespace DevXpert.Modulo3.Core.Mediator;

public class MediatrHandler(IMediator mediator) : IMediatrHandler
{
    public async Task<bool> EnviarComando<T>(T comando) where T : Command
    {
        return await mediator.Send(comando);
    }

    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        await mediator.Publish(evento);
    }

    public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
    {
        await mediator.Publish(notificacao);
    }

    public async Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent
    {
        await mediator.Publish(notificacao);
    }
}

