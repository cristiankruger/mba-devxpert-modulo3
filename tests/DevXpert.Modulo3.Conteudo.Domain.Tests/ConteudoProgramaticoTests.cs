using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.ModuloConteudo.Domain.Tests.Fixture;
using Shouldly;

namespace DevXpert.Modulo3.ModuloConteudo.Domain.Tests;

[Collection(nameof(CursoCollection))]
[Trait("Teste Unidade", "ConteudoDomain ConteudoProgramatico")]
public class ConteudoProgramaticoTests(CursoFixture cursoFixture)
{
    private readonly CursoFixture _cursosFixture = cursoFixture;

    [Fact]
    public void ConteudoProgramatico_Validar_ObjetoNaoEhEntidade()
    {
        var result = _cursosFixture.GerarConteudoProgramaticoValido();

        result.ShouldBeOfType<ConteudoProgramatico>();
        result.ShouldNotBeAssignableTo<Entity>();
        result.ShouldNotBeAssignableTo<IAggregateRoot>();
    }

    [Fact]
    public void ConteudoProgramatico_Validar_ValidacoesInstrutorDevemRetornarException()
    {
        var ementa = _cursosFixture.GerarEmentaValida();
        var publicoAlvo = _cursosFixture.GerarPublicoAlvoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeConteudoProgramaticoInvalido(_cursosFixture.GerarNomeInvalido(true), ementa, publicoAlvo)
        );

        ex.Message.ShouldBe(ConteudoProgramatico.InstrutorVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeConteudoProgramaticoInvalido(_cursosFixture.GerarNomeInvalido(), ementa, publicoAlvo)
        );

        ex.Message.ShouldBe(ConteudoProgramatico.InstrutorLengthMsgErro);
    }

    [Fact]
    public void ConteudoProgramatico_Validar_ValidacoesEmentaDevemRetornarException()
    {
        var instrutor = _cursosFixture.GerarInstrutorValido();
        var publicoAlvo = _cursosFixture.GerarPublicoAlvoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeConteudoProgramaticoInvalido(instrutor, "", publicoAlvo)
        );

        ex.Message.ShouldBe(ConteudoProgramatico.EmentaVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeConteudoProgramaticoInvalido(instrutor, _cursosFixture.GerarNomeInvalido(), publicoAlvo)
        );

        ex.Message.ShouldBe(ConteudoProgramatico.EmentaLengthMsgErro);
    }

    [Fact]
    public void ConteudoProgramatico_Validar_ValidacoesPublicoAlvoDevemRetornarException()
    {
        var instrutor = _cursosFixture.GerarInstrutorValido();
        var ementa = _cursosFixture.GerarEmentaValida();

        var ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeConteudoProgramaticoInvalido(instrutor, ementa, "")
        );

        ex.Message.ShouldBe(ConteudoProgramatico.PublicoAlvoVazioMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeConteudoProgramaticoInvalido(instrutor, ementa, _cursosFixture.GerarNomeInvalido())
        );

        ex.Message.ShouldBe(ConteudoProgramatico.PublicoAlvoLengthMsgErro);
    }


}

