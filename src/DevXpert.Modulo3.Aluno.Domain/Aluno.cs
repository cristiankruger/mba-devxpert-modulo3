using DevXpert.Modulo3.Core.DomainObjects;
using System.Text.RegularExpressions;

namespace DevXpert.Modulo3.ModuloAluno.Domain;

public class Aluno : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    
    private readonly List<Matricula> _matriculas;
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas;


    protected Aluno()
    {
        _matriculas = [];
    }

    public Aluno(string nome, string email)
    {
        Nome = nome;
        Email = email;
        _matriculas = [];
        Ativar();
        Validar();
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, NomeVazioMsgErro);
        Validacoes.ValidarMinimoMaximo(Nome, 10, 100, NomeLenghtMsgErro);
        Validacoes.ValidarSeDiferente(Nome, @"^[a-zà-ÿ]+(?:[ '-][a-zà-ÿ]+)*$", NomeRegexMsgErro, RegexOptions.IgnoreCase);
        Validacoes.ValidarSeVazio(Email, EmailVazioMsgErro);
        Validacoes.ValidarSeDiferente(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", EmailRegexMsgErro, RegexOptions.IgnoreCase);
    }

    //MENSAGENS VALIDACAO
    public const string NomeVazioMsgErro = "Nome do Aluno não pode ser vazio.";
    public const string NomeLenghtMsgErro = "Nome do Aluno deve ter entre 10 e 100 caracteres.";
    public const string NomeRegexMsgErro = "Nome do Aluno deve ser um nome válido.";
    public const string EmailVazioMsgErro = "E-mail do Aluno não pode ser vazio.";
    public const string EmailRegexMsgErro = "E-mail do Aluno deve ser um e-mail válido.";    
}
