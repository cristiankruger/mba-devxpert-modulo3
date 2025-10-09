using DevXpert.Modulo3.Core.Messages;

namespace DevXpert.Modulo3.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public bool Ativo{ get; protected set; }
    public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();
    private List<Event> _notificacoes;

    protected Entity()
    {
        Id = Guid.NewGuid();        
    }

    protected void Ativar()
    {
        Ativo = true;
    }

    protected void Desativar()
    {
        Ativo = false;
    }

    public void AdicionarEvento(Event evento)
    {
        _notificacoes ??= [];
        _notificacoes.Add(evento);
    }

    public void RemoverEvento(Event eventItem)
    {
        _notificacoes?.Remove(eventItem);
    }

    public void LimparEventos()
    {
        _notificacoes?.Clear();
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (compareTo is null) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}
