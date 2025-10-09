using DevXpert.Modulo3.Core.Messages;
using MediatR;

namespace DevXpert.Modulo3.Core.Bus;

public class MediatrHandler(IMediator mediator) : IMediatrHandler
{
    public async Task PublicarEvento<T>(T evento) where T : Event
    {
        await mediator.Publish(evento);
    }
}

