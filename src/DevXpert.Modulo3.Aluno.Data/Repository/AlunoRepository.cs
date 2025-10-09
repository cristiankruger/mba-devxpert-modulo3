using DevXpert.Modulo3.Aluno.Domain;
using DevXpert.Modulo3.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.Aluno.Data.Repository
{
    public class AlunoRepository(AlunoContext context) : IAlunoRepository
    {
        public IUnitOfWork UnitOfWork => context;

        public async Task<Domain.Aluno> Obter(Guid id)
        {
            return await context.Alunos.FindAsync(id);
        }

        public async Task<Matricula> ObterMatricula(Guid id)
        {
            return await context.Matriculas.FindAsync(id);
        }

        public async Task<Certificado> ObterCertificado(Guid matriculaId)
        {
            return await context.Certificados
                                .Include(c => c.Matricula)
                                .FirstOrDefaultAsync(c => c.MatriculaId == matriculaId);
        }

        public async Task<IEnumerable<Matricula>> ObterListaMatricula(Guid alunoId)
        {
            return await context.Matriculas
                                .AsNoTracking()
                                .Where(m => m.AlunoId == alunoId)
                                .ToListAsync();
        }


        public async Task Adicionar(Domain.Aluno aluno)
        {
            await context.Alunos.AddAsync(aluno);
        }

        public async Task Adicionar(Matricula matricula)
        {
            await context.Matriculas.AddAsync(matricula);
        }

        public async Task Adicionar(Certificado certificado)
        {
            await context.Certificados.AddAsync(certificado);
        }

        public void Atualizar(Domain.Aluno aluno)
        {
            context.Alunos.Update(aluno);
        }
    }
}
