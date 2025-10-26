using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloConteudo.Domain;

public class Aula : Entity
{
    public Guid CursoId { get; private set; }
    public string Conteudo { get; private set; }
    public string Material { get; private set; }
   
    /*EF Relation*/
    public Curso Curso { get; set; }
    /*EF Relation*/

    protected Aula() { }

    public Aula(Guid cursoId, string conteudo, string material)
    {
        CursoId = cursoId;
        Conteudo = conteudo;
        Material = material;
        Ativar();
        Validar();
    }
    
    public Aula(Guid id, Guid cursoId, string conteudo, string material)
    {
        Id = id;    
        CursoId = cursoId;
        Conteudo = conteudo;
        Material = material;
        Ativar();
        Validar();
    }

   
    public void Validar()
    {
        Validacoes.ValidarSeIgual(CursoId, Guid.Empty, CursoIdMsgErro);
        Validacoes.ValidarSeVazio(Conteudo, ConteudoMsgErro);
        Validacoes.ValidarMinimoMaximo(Conteudo, 5, 100, ConteudoLengthMsgErro);
        Validacoes.ValidarSeMaiorQue(Material, 100, MaterialLengthMsgErro, true);
    }

    //MENSAGENS VALIDACAO
    public const string CursoIdMsgErro = "O campo CursoId da aula não pode estar vazio.";
    public const string ConteudoMsgErro = "O campo Título da aula não pode estar vazio.";
    public const string ConteudoLengthMsgErro = "O campo Título da aula deve ter entre 5 e 100 caracteres.";
    public const string MaterialLengthMsgErro = "O campo Material da aula deve ter no máximo 100 caracteres.";
}
