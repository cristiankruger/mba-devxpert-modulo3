using DevXpert.Modulo3.ModuloConteudo.Domain;
using DevXpert.Modulo3.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevXpert.Modulo3.ModuloConteudo.Data.Repository;

public class CursoRepository(CursoContext context) : ICursoRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<Curso> Obter(Guid id)
    {
        return await context.Cursos
                            .Include(c => c.Aulas)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Aula> ObterAula(Guid id)
    {
        return await context.Aulas
                            .Include(c => c.Curso)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Aula>> ObterAulas(Guid cursoId)
    {
        return await context.Aulas
                            .Include(a => a.Curso)
                            .AsNoTracking()
                            .Where(a => a.CursoId == cursoId)
                            .ToListAsync();
    }

    public async Task<IEnumerable<Curso>> ObterTodos()
    {
        return await context.Cursos
                            .Include(c => c.Aulas)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<IEnumerable<Curso>> Buscar(Expression<Func<Curso, bool>> predicado)
    {
        return await context.Cursos
                            .Include(a => a.Aulas)
                            .AsNoTracking()
                            .Where(predicado)
                            .ToListAsync();
    }

    public async Task Adicionar(Curso curso)
    {
        await context.Cursos.AddAsync(curso);
    }

    public async Task Adicionar(Aula aula)
    {
        await context.Aulas.AddAsync(aula);
    }

    public void Atualizar(Curso curso)
    {
        context.Cursos.Update(curso);
    }

    public void Atualizar(Aula aula)
    {
        context.Aulas.Update(aula);
    }

}
