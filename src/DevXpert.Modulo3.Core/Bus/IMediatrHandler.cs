using DevXpert.Modulo3.Core.Messages;

namespace DevXpert.Modulo3.Core.Bus;

public interface IMediatrHandler
{
    Task PublicarEvento<T>(T evento) where T : Event;
}
