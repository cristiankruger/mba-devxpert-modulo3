namespace DevXpert.Modulo3.Core.Data;

public  interface IUnitOfWork
{
    Task<bool> Commit();
}
