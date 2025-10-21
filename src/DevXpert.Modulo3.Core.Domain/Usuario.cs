using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.Core.Domain;

public abstract class Usuario : Entity, IAggregateRoot
{
    public string Email { get; set; }
    public string Nome { get; set; }

}
