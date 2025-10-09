using DevXpert.Modulo3.Core.Messages;

namespace DevXpert.Modulo3.Core.DomainObjects;

public class DomainEvent : Event
{
    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}
