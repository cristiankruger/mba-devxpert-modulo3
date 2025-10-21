using DevXpert.Modulo3.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.Core.Data.Repository;

public class AlunoRepository(IdentityAppContext context) : IAlunoRepository
{
    public IUnitOfWork UnitOfWork => context;

    //LEITURA
    public async Task<Aluno> Obter(string email)
    {
        return await context.Alunos.FirstOrDefaultAsync(u => u.Email == email && u.Ativo);
    }   

    //ESCRITA
    public async Task<bool> Adicionar(Aluno aluno)
    {
        await context.AddAsync(aluno);
        return true;
    }
}
