using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloFinanceiro.Domain;

public class DadosCartao
{
    public string Titular { get; private set; }
    public string NumeroCartao { get; private set; }
    public string Cvv { get; private set; }
    public string DataValidade { get; private set; }

    //MENSAGENS VALIDAÇÃO
    public const string TitularVazioMsgErro = "O campo Titular do Cartão não pode estar vazio.";
    public const string TitularRegexMsgErro = "O campo Titular do Cartão só pode conter letras e espaços.";
    public const string TitularTamanhoMsgErro = "O campo Titular do Cartão deve ter entre 5 e 100 caracteres.";
    public const string NumeroCartaoVazioMsgErro = "O campo Titular do Cartão não pode estar vazio.";
    public const string NumeroCartaoRegexMsgErro = "O campo Número do Cartão só pode conter dígitos.";
    public const string NumeroCartaoTamanhoMsgErro = "O campo Número do Cartão deve ter entre 12 e 20 dígitos.";
    public const string CvvVazioMsgErro = "O campo CVV do Cartão não pode estar vazio.";
    public const string CvvRegexMsgErro = "O campo CVV do Cartão deve conter 3 ou 4 dígitos dígitos.";
    public const string DataValidadeVazioMsgErro = "O campo Data de Validade do Cartão não pode estar vazio.";
    public const string DataValidadeRegexMsgErro = "O campo Data de Validade deve estar no formato MM/AAAA.";
    public const string DataValidadeVencidoMsgErro = "O campo Data de Validade deve ser superior à data atual.";
    
    public DadosCartao(string titular, string numeroCartao, string cvv, string dataValidade)
    {
        Validacoes.ValidarSeVazio(titular, TitularVazioMsgErro);
        Validacoes.ValidarSeDiferente(titular, @"^[A-Za-z\s]+$", TitularRegexMsgErro);
        Validacoes.ValidarTamanho(titular, 5, 100, TitularTamanhoMsgErro);
        Validacoes.ValidarSeVazio(numeroCartao, NumeroCartaoVazioMsgErro);
        Validacoes.ValidarSeDiferente(numeroCartao, @"^\d+$", NumeroCartaoRegexMsgErro);
        Validacoes.ValidarMinimoMaximo(numeroCartao, 12, 20, NumeroCartaoTamanhoMsgErro);
        Validacoes.ValidarSeVazio(cvv, CvvVazioMsgErro);
        Validacoes.ValidarSeDiferente(cvv, @"^\d{3,4}$", CvvRegexMsgErro);
        Validacoes.ValidarSeVazio(dataValidade, DataValidadeVazioMsgErro);
        Validacoes.ValidarSeDiferente(dataValidade, @"^(0[1-9]|1[0-2])\/(20)\d{2}$", DataValidadeRegexMsgErro);
        Validacoes.ValidarSeMenorQue(dataValidade, '/', DataValidadeVencidoMsgErro);

        NumeroCartao = numeroCartao;
        Titular = titular;
        DataValidade = dataValidade;
        Cvv = cvv;
    }

    public string CartaoFormatado()
    {
        return $"Cartão: **** **** **** {NumeroCartao.Substring(NumeroCartao.Length - 4, 4)}; Titular: {Titular}; Valido até: {DataValidade}";
    }

    public override string ToString()
    {
        return CartaoFormatado();
    }        

}
