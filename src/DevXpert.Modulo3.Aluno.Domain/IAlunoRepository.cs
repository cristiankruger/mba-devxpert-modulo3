using DevXpert.Modulo3.Core.Data;

namespace DevXpert.Modulo3.Aluno.Domain;

public interface IAlunoRepository : IRepository<Aluno>
{
    Task<Aluno> Obter(Guid id);
    Task<Matricula> ObterMatricula(Guid id);
    Task<Certificado> ObterCertificado(Guid matriculaId);
    Task<IEnumerable<Matricula>> ObterListaMatricula(Guid alunoId);    

    Task Adicionar(Aluno aluno);
    void Atualizar(Aluno aluno);
    Task Adicionar(Matricula matricula);
    Task Adicionar(Certificado certificado);
}
