using DevXpert.Modulo3.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.Core.Data.Repository;

public class AdminRepository(IdentityAppContext context) : IAdminRepository
{
    public IUnitOfWork UnitOfWork => context;

    //LEITURA
    public async Task<Admin> Obter(string email)
    {
        return await context.Admins.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
    }   

    //ESCRITA
    public async Task<bool> Adicionar(Admin admin)
    {
        await context.AddAsync(admin);
        return true;
    }
}
