using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloAluno.Domain;

public class Certificado : Entity
{
    public Guid MatriculaId { get; private set; }
    
    public DateTime DataEmissao{ get; private set; }

    /*EF Relation*/
    public Matricula Matricula{ get; set; }
    /*EF Relation*/

    protected Certificado() { }

    public Certificado(Guid matriculaId)
    {
        MatriculaId = matriculaId;
        DataEmissao = DateTime.Now;

        Validar();
    }

    public void Validar()
    {
        Validacoes.ValidarSeIgual(MatriculaId, Guid.Empty, MatriculaIdMsgErro);
    }

    //MENSAGENS VALIDACAO
    public const string MatriculaIdMsgErro = "O campo MatriculaId do Certificado não pode estar vazio.";
}
