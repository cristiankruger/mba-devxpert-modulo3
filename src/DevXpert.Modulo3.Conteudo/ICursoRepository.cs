using DevXpert.Modulo3.Core.Data;

namespace DevXpert.Modulo3.Conteudo.Domain;

public interface ICursoRepository : IRepository<Curso>
{
    Task<Curso> Obter(Guid id);
    Task<IEnumerable<Curso>> ObterTodos();
    Task<Aula> ObterAula(Guid id);
    Task<IEnumerable<Aula>> ObterAulas(Guid cursoId);
    Task Adicionar(Curso curso);
    void Atualizar(Curso curso);
    Task Adicionar(Aula aula);
    void Atualizar(Aula aula);
}
