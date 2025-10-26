using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.Financeiro.Domain.Tests._Fixture;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;
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
    public void DadosCartao_Validar_ValidacoesNomeCartaoDevemRetornarException()
    {
        var numeroCartao = _dadosFixture.GerarNumeroCartaoValido();
        var cvv = _dadosFixture.GerarCvvValido();
        var data = _dadosFixture.GerarDataValidadeValida();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido("", numeroCartao, cvv, data)
        );

        ex.Message.ShouldBe(DadosCartao.NomeCartaoVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido("Jose 123 @", numeroCartao, cvv, data)
        );

        ex.Message.ShouldBe(DadosCartao.NomeCartaoRegexMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido("z e", numeroCartao, cvv, data)
        );

        ex.Message.ShouldBe(DadosCartao.NomeCartaoTamanhoMsgErro);
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesNumeroCartaoDevemRetornarException()
    {
        var nomeCartao = _dadosFixture.GerarNomeCartaoValido();
        var cvv = _dadosFixture.GerarCvvValido();
        var data = _dadosFixture.GerarDataValidadeValida();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, "", cvv, data)
        );

        ex.Message.ShouldBe(DadosCartao.NumeroCartaoVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, "123456", cvv, data)
        );

        ex.Message.ShouldBe(DadosCartao.NumeroCartaoTamanhoMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, "1234 Jo #", cvv, data)
        );

        ex.Message.ShouldBe(DadosCartao.NumeroCartaoRegexMsgErro);
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesCvvDevemRetornarException()
    {
        var nomeCartao = _dadosFixture.GerarNomeCartaoValido();
        var numeroCartao = _dadosFixture.GerarNumeroCartaoValido();
        var data = _dadosFixture.GerarDataValidadeValida();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, numeroCartao, "", data)
        );

        ex.Message.ShouldBe(DadosCartao.CvvVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, numeroCartao, "12345", data)
        );

        ex.Message.ShouldBe(DadosCartao.CvvRegexMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, numeroCartao, "A1@", data)
        );

        ex.Message.ShouldBe(DadosCartao.CvvRegexMsgErro);
    }

    [Fact]
    public void DadosCartao_Validar_ValidacoesDataDevemRetornarException()
    {
        var nomeCartao = _dadosFixture.GerarNomeCartaoValido();
        var numeroCartao = _dadosFixture.GerarNumeroCartaoValido();
        var cvv = _dadosFixture.GerarCvvValido();

        var ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, numeroCartao, cvv, "")
        );

        ex.Message.ShouldBe(DadosCartao.DataValidadeVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, numeroCartao, cvv, "0J/2026")
        );

        ex.Message.ShouldBe(DadosCartao.DataValidadeRegexMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _dadosFixture.GerarFakeDadosCartaoInvalido(nomeCartao, numeroCartao, cvv, "07/2025")
        );

        ex.Message.ShouldBe(DadosCartao.DataValidadeVencidoMsgErro);
    }
}
