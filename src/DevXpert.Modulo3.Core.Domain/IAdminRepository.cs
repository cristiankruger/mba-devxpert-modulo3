using DevXpert.Modulo3.Core.Data;

namespace DevXpert.Modulo3.Core.Domain
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> Obter(string email);
        Task<bool> Adicionar(Admin admin);
    }
}
