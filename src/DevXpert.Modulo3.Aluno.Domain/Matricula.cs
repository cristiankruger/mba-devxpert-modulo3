using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloAluno.Domain;

public class Matricula : Entity
{
    public string CursoNome { get; private set; }
    public decimal Valor { get; private set; }
    public Guid CursoId { get; private set; }
    public Guid AlunoId { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public DateTime? DataConclusao { get; private set; }
    public StatusMatriculaEnum Status { get; private set; }

    /*EF Relation*/
    public Aluno Aluno { get; set; }
    public Certificado Certificado { get; set; }
    /*EF Relation*/

    protected Matricula() { }

    public Matricula(string cursoNome, decimal valor, Guid cursoId, Guid alunoId)
    {
        CursoNome = cursoNome;
        Valor = valor;
        CursoId = cursoId;
        AlunoId = alunoId;
        DataCadastro = DateTime.Now;
        Status = StatusMatriculaEnum.PendentePagamento;
        Validar();
    }

    public void Matricular()
    {
        Status = StatusMatriculaEnum.Matriculado;
    }

    public void Concluir()
    {
        Status = StatusMatriculaEnum.Concluido;
        DataConclusao = DateTime.Now;
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(CursoNome, CursoNomeMsgErro);
        Validacoes.ValidarMinimoMaximo(CursoNome, 10, 100, CursoNomeLengthMsgErro);
        Validacoes.ValidarSeIgual(CursoId, Guid.Empty, CursoIdMsgErro);
        Validacoes.ValidarSeIgual(AlunoId, Guid.Empty, AlunoIdMsgErro);

    }

    //MENSAGENS VALIDACAO
    public const string CursoNomeMsgErro = "O campo Nome do Curso na matrícula não pode estar vazio.";
    public const string CursoNomeLengthMsgErro = "O campo Nome do curso na matrícula deve ter entre 10 e 100 caracteres.";
    public const string CursoIdMsgErro = "O campo CursoId da matrícula não pode estar vazio.";
    public const string AlunoIdMsgErro = "O campo AlunoId da matrícula não pode estar vazio.";
}
