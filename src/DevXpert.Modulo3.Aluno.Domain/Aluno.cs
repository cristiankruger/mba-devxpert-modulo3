using DevXpert.Modulo3.Core.DomainObjects;
using System.Text.RegularExpressions;

namespace DevXpert.Modulo3.Aluno.Domain;

public class Aluno : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }
    public DateTime DataNascimento { get; private set; }
    private readonly List<Matricula> _matriculas;
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas;
    

    protected Aluno()
    {    
        _matriculas= [];
    }

    public Aluno(string nome, string email, string cpf, DateTime dataNascimento)
    {
        Nome = nome;
        Email = email;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        _matriculas = [];
        Ativar();
        Validar();
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, NomeVazioMsgErro);
        Validacoes.ValidarMinimoMaximo(Nome, 10,100, NomeLenghtMsgErro);
        Validacoes.ValidarSeDiferente(Nome, @"^[a-zà-ÿ]+(\\s?[a-zà-ÿ][-'.]?\\s?)*([a-zà-ÿ]|[jr.|I|II|III|IV]?)*$", NomeRegexMsgErro, RegexOptions.IgnoreCase);
        Validacoes.ValidarSeVazio(Cpf, CpfVazioMsgErro);
        Validacoes.ValidarMinimoMaximo(Cpf, 11, 11, CpfLenghtMsgErro);
        Validacoes.ValidarSeDiferente(Cpf, @"^\d{11}$", CpfRegexMsgErro);
        Validacoes.ValidarSeVazio(Email, EmailVazioMsgErro);
        Validacoes.ValidarSeDiferente(Email, "^[a-z0-9-._]+@[a-z0-9_-]+?\\.[a-z.-]{5,50}$", EmailRegexMsgErro);
        Validacoes.ValidarSeVerdadeiro(DataNascimento < DateTime.Now.AddYears(-18),DataNascimentoMsgErro);
    }

    //MENSAGENS VALIDACAO
    public const string NomeVazioMsgErro = "Nome do Aluno não pode ser vazio.";
    public const string NomeLenghtMsgErro = "Nome do Aluno deve ter entre 10 e 100 caracteres.";
    public const string NomeRegexMsgErro = "Nome do Aluno deve ser um nome válido.";
    public const string CpfVazioMsgErro = "CPF do Aluno não pode ser vazio.";
    public const string CpfLenghtMsgErro = "CPF do Aluno deve conter 11 dígitos.";
    public const string CpfRegexMsgErro = "CPF do Aluno deve conter apenas dígitos.";
    public const string EmailVazioMsgErro = "E-mail do Aluno não pode ser vazio.";
    public const string EmailRegexMsgErro = "E-mail do Aluno deve ser um e-mail válido.";
    public const string DataNascimentoMsgErro = "Aluno deve ser maior de idade.";
}
