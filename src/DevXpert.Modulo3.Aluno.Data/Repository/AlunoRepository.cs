using DevXpert.Modulo3.ModuloAluno.Domain;
using DevXpert.Modulo3.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.ModuloAluno.Data.Repository
{
    public class AlunoRepository(AlunoContext context) : IAlunoRepository
    {
        public IUnitOfWork UnitOfWork => context;

        public async Task<Aluno> Obter(Guid id)
        {
            return await context.Alunos.FindAsync(id);
        }

        public async Task<Matricula> ObterMatricula(Guid id)
        {
            return await context
                            .Matriculas
                            .Include(a => a.Certificado)
                            .Include(a => a.Aluno)
                            .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Certificado> ObterCertificado(Guid matriculaId)
        {
            return await context.Certificados
                                .Include(c => c.Matricula)
                                .ThenInclude(c => c.Aluno)
                                .FirstOrDefaultAsync(c => c.MatriculaId == matriculaId);
        }

        public async Task<IEnumerable<Matricula>> ObterListaMatricula(Guid alunoId)
        {
            return await context.Matriculas
                                .AsNoTracking()
                                .Where(m => m.AlunoId == alunoId)
                                .ToListAsync();
        }


        public async Task Adicionar(Aluno aluno)
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
    }
}
