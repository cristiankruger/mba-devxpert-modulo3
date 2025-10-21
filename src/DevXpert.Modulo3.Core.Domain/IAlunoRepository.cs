using DevXpert.Modulo3.Core.Data;

namespace DevXpert.Modulo3.Core.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<Aluno> Obter(string email);
        Task<bool> Adicionar(Aluno aluno);
    }
}
