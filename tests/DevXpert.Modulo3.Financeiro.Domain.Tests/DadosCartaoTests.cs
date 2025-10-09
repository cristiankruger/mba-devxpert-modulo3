using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.Financeiro.Domain.Tests._Fixture;
using Shouldly;

namespace DevXpert.Modulo3.Financeiro.Domain.Tests;

[Collection(nameof(DadosCartaoCollection))]
[Trait("Teste Unidade", "FinanceiroDomain DadosCartao")]
public class DadosCartaoTests(DadosCartaoTestsFixture dadosCartaoTestsFixture)
{
    private readonly DadosCartaoTestsFixture _dadosFixture = dadosCartaoTestsFixture;

    [Fact]
    public void DadosCartao_Validar_ObjetoNaoEhEntidade()
    {
        var result = _dadosFixture.GerarDadosCartaoValido();

        result.ShouldBeOfType<DadosCartao>();
        result.ShouldNotBeAssignableTo<Entity>();        
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesTitularDevemRetornarException()
    {
        var numeroCartao = _dadosFixture.GerarNumeroCartaoValido();
        var cvv = _dadosFixture.GerarCvvValido();
        var data = _dadosFixture.GerarDataValidadeValida();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido("", numeroCartao, cvv, data)
        );

        Assert.Equal(DadosCartao.TitularVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido("Jose 123 @", numeroCartao, cvv, data)
        );

        Assert.Equal(DadosCartao.TitularRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido("z e", numeroCartao, cvv, data)
        );

        Assert.Equal(DadosCartao.TitularTamanhoMsgErro, ex.Message);
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesNumeroCartaoDevemRetornarException()
    {
        var titular = _dadosFixture.GerarTitularValido();
        var cvv = _dadosFixture.GerarCvvValido();
        var data = _dadosFixture.GerarDataValidadeValida();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, "", cvv, data)
        );

        Assert.Equal(DadosCartao.NumeroCartaoVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, "123456", cvv, data)
        );

        Assert.Equal(DadosCartao.NumeroCartaoTamanhoMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, "1234 Jo #", cvv, data)
        );

        Assert.Equal(DadosCartao.NumeroCartaoRegexMsgErro, ex.Message);
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesCvvDevemRetornarException()
    {
        var titular = _dadosFixture.GerarTitularValido();
        var numeroCartao = _dadosFixture.GerarNumeroCartaoValido();
        var data = _dadosFixture.GerarDataValidadeValida();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, numeroCartao, "", data)
        );

        Assert.Equal(DadosCartao.CvvVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, numeroCartao, "1234", data)
        );

        Assert.Equal(DadosCartao.CvvRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, numeroCartao, "A1@", data)
        );

        Assert.Equal(DadosCartao.CvvRegexMsgErro, ex.Message);
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesDataDevemRetornarException()
    {
        var titular = _dadosFixture.GerarTitularValido();
        var numeroCartao = _dadosFixture.GerarNumeroCartaoValido();
        var cvv = _dadosFixture.GerarCvvValido();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, numeroCartao, cvv, "")
        );

        Assert.Equal(DadosCartao.DataValidadeVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, numeroCartao, cvv, "0J/2026")
        );

        Assert.Equal(DadosCartao.DataValidadeRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(titular, numeroCartao, cvv, "07/2025")
        );

        Assert.Equal(DadosCartao.DataValidadeVencidoMsgErro, ex.Message);
    }
}
