using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.Conteudo.Domain;

public class Aula : Entity
{
    public Guid CursoId { get; private set; }
    public string Link { get; private set; }
    public string Titulo { get; private set; }
    public TimeSpan Duracao { get; private set; }
    //TODO: Tratar no futuro o link como arquivo de upload (IFormFile)

    /*EF Relation*/
    public Curso Curso { get; set; }
    /*EF Relation*/

    protected Aula() { }

    public Aula(Guid cursoId, string link, TimeSpan duracao)
    {
        CursoId = cursoId;
        Link =link;
        Duracao = duracao;
        Ativar();
        Validar();
    }

    public void Validar()
    {
        Validacoes.ValidarSeIgual(CursoId, Guid.Empty, CursoIdMsgErro);
        Validacoes.ValidarSeMenorQue(Duracao.Ticks, 1, DuracaoMsgErro);
        Validacoes.ValidarSeVazio(Titulo, TituloMsgErro);
        Validacoes.ValidarMinimoMaximo(Titulo, 10, 100, TituloLengthMsgErro);
        Validacoes.ValidarMinimoMaximo(Titulo, 10, 250, TituloLengthMsgErro);
        Validacoes.ValidarSeVazio(Link, LinkMsgErro);
    }

    //MENSAGENS VALIDACAO
    public const string CursoIdMsgErro = "O campo CursoId da aula não pode estar vazio.";
    public const string DuracaoMsgErro = "O campo Duração da aula deve ser superior a 0.";
    public const string TituloMsgErro = "O campo Título da aula não pode estar vazio.";
    public const string TituloLengthMsgErro = "O campo Título da aula deve ter entre 10 e 100 caracteres.";
    public const string LinkMsgErro = "O campo Link da aula não pode estar vazio.";
    public const string LinkLengthMsgErro = "O campo Link da aula deve ter entre 10 e 250 caracteres.";
}
