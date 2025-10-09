using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.Core.Data;

public  interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
