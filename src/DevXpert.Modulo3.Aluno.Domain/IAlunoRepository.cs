using DevXpert.Modulo3.Core.Data;

namespace DevXpert.Modulo3.ModuloAluno.Domain;

public interface IAlunoRepository : IRepository<Aluno>
{
    Task<Aluno> Obter(Guid id);
    Task<Matricula> ObterMatricula(Guid id);
    Task<IEnumerable<Matricula>> ObterListaMatricula(Guid alunoId);
    Task<Certificado> ObterCertificado(Guid matriculaId);

    Task Adicionar(Aluno aluno);    
    Task Adicionar(Matricula matricula);
    Task Adicionar(Certificado certificado);
}
