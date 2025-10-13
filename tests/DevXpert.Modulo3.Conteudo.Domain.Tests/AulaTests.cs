using DevXpert.Modulo3.Conteudo.Domain.Tests.Fixture;
using DevXpert.Modulo3.Core.DomainObjects;
using Shouldly;

namespace DevXpert.Modulo3.Conteudo.Domain.Tests;

[Collection(nameof(AulaCollection))]
[Trait("Teste Unidade", "ConteudoDomain Aula")]
public class AulaTests(AulaFixture aulaFixture)
{
    private readonly AulaFixture _aulasFixture = aulaFixture;

    [Fact]
    public void Aula_Validar_ObjetoNaoEhEntidade()
    {
        var result = _aulasFixture.GerarAulaValido();

        result.ShouldBeOfType<Aula>();
        result.ShouldBeAssignableTo<Entity>();
        result.ShouldNotBeAssignableTo<IAggregateRoot>();
    }

    [Fact]
    public void Aula_Validar_ValidacoesCursoIdDevemRetornarException()
    {
        var titulo = _aulasFixture.GerarTituloValido();
        var link = _aulasFixture.GerarLinkValido();
        var duracao = _aulasFixture.GerarDuracaoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.Empty, titulo, link, duracao)
        );

        ex.Message.ShouldBe(Aula.CursoIdMsgErro);
    }

    [Fact]
    public void Aula_Validar_ValidacoesTituloDevemRetornarException()
    {
        var link = _aulasFixture.GerarLinkValido();
        var duracao = _aulasFixture.GerarDuracaoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), "", link, duracao)
        );

        ex.Message.ShouldBe(Aula.TituloMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), "A", link, duracao)
        );

        ex.Message.ShouldBe(Aula.TituloLengthMsgErro);
    }

    [Fact]
    public void Aula_Validar_ValidacoesLinkDevemRetornarException()
    {
        var titulo = _aulasFixture.GerarTituloValido();
        var duracao = _aulasFixture.GerarDuracaoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), titulo, "", duracao)
        );

        ex.Message.ShouldBe(Aula.LinkMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), titulo, _aulasFixture.GerarLinkInvalido(), duracao)
        );

        ex.Message.ShouldBe(Aula.LinkLengthMsgErro);
    }

    [Fact]
    public void Aula_Validar_ValidacoesDuracaoDevemRetornarException()
    {
        var titulo = _aulasFixture.GerarTituloValido();
        var link = _aulasFixture.GerarLinkValido();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), titulo, link, TimeSpan.FromSeconds(0))
        );

        ex.Message.ShouldBe(Aula.DuracaoMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), titulo, link, TimeSpan.FromSeconds(10000))
        );

        ex.Message.ShouldBe(Aula.DuracaoMsgErro);
    }
}
