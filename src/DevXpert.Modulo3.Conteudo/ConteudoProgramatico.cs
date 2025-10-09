using DevXpert.Modulo3.Core.DomainObjects;
using System.Text.RegularExpressions;

namespace DevXpert.Modulo3.Conteudo.Domain;

public class ConteudoProgramatico
{
    public string Ementa { get; private set; }
    public string Instrutor { get; private set; }
    public string PublicoAlvo { get; private set; }    
    
    public ConteudoProgramatico(string ementa, string instrutor, string publicoAlvo)
    {
        Validacoes.ValidarSeVazio(ementa, EmentaVazioMsgErro);
        Validacoes.ValidarMinimoMaximo(ementa, 20, 1000, EmentaLengthMsgErro);
        Validacoes.ValidarSeVazio(instrutor, InstrutorVazioMsgErro);
        Validacoes.ValidarMinimoMaximo(instrutor, 5, 100, InstrutorLengthMsgErro);
        Validacoes.ValidarSeDiferente(instrutor, @"^[a-zà-ÿ]+(\\s?[a-zà-ÿ][-'.]?\\s?)*([a-zà-ÿ]|[jr.|I|II|III|IV]?)*$", InstrutorRegexMsgErro, RegexOptions.IgnoreCase);
        Validacoes.ValidarSeVazio(publicoAlvo, PublicoAlvoVazioMsgErro);
        Validacoes.ValidarMinimoMaximo(publicoAlvo, 5, 250, PublicoAlvoLengthMsgErro);

        Ementa = ementa;
        Instrutor = instrutor;
        PublicoAlvo = publicoAlvo;
    }

    public string ConteudoFormatado()
    {
        return $"Ementa: {Ementa}; Instrutor: {Instrutor}; Público Alvo: {PublicoAlvo}";
    }
    public override string ToString()
    {
        return ConteudoFormatado();
    }

    //MENSAGENS VALIDAÇÃO
    public const string EmentaVazioMsgErro = "O campo Ementa não pode estar vazio.";
    public const string EmentaLengthMsgErro = "O campo Ementa deve ter entre 20 e 1000 caracteres.";
    public const string InstrutorVazioMsgErro = "O campo Nome do Instrutor não pode estar vazio.";
    public const string InstrutorLengthMsgErro = "O campo Nome do Instrutor deve ter entre 5 e 100 caracteres.";
    public const string InstrutorRegexMsgErro = "O campo Nome do Instrutor deve ser um nome próprio.";
    public const string PublicoAlvoVazioMsgErro = "O campo Público Alvo não pode estar vazio.";
    public const string PublicoAlvoLengthMsgErro = "O campo Ementa deve ter entre 5 e 250 caracteres.";
}