using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.ModuloConteudo.Domain.Tests.Fixture;
using Shouldly;

namespace DevXpert.Modulo3.ModuloConteudo.Domain.Tests;

[Collection(nameof(AulaCollection))]
[Trait("Teste Unidade", "ConteudoDomain Aula")]
public class AulaTests(AulaFixture aulaFixture)
{
    private readonly AulaFixture _aulasFixture = aulaFixture;

    [Fact(DisplayName = "Aula Validar ObjetoNaoEhEntidade")]
    public void Aula_Validar_ObjetoNaoEhEntidade()
    {
        var result = _aulasFixture.GerarAulaValido();

        result.ShouldBeOfType<Aula>();
        result.ShouldBeAssignableTo<Entity>();
        result.ShouldNotBeAssignableTo<IAggregateRoot>();
    }

    [Fact(DisplayName = "Aula Validar ValidacoesCursoIdDevemRetornarException")]
    public void Aula_Validar_ValidacoesCursoIdDevemRetornarException()
    {
        var conteudo = _aulasFixture.GerarConteudoValido();
        var material = _aulasFixture.GerarMaterialValido();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.Empty, conteudo, material)
        );

        ex.Message.ShouldBe(Aula.CursoIdMsgErro);
    }

    [Fact(DisplayName = "Aula Validar ValidacoesConteudoDevemRetornarException")]
    public void Aula_Validar_ValidacoesConteudoDevemRetornarException()
    {
        var conteudoInvalido = _aulasFixture.GerarConteudoInvalido();
        var material = _aulasFixture.GerarMaterialValido();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), "", material)
        );

        ex.Message.ShouldBe(Aula.ConteudoMsgErro);

        ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(), conteudoInvalido, material)
        );

        ex.Message.ShouldBe(Aula.ConteudoLengthMsgErro);
    }

    [Fact(DisplayName = "Aula Validar ValidacoesMaterialDevemRetornarException")]
    public void Aula_Validar_ValidacoesMaterialDevemRetornarException()
    {
        var conteudo = _aulasFixture.GerarConteudoValido();
        var material = _aulasFixture.GerarMaterialInvalido();

        var ex = Assert.Throws<DomainException>(() =>
            _aulasFixture.GerarFakeAulaInvalido(Guid.NewGuid(),conteudo, material)
        );


        ex.Message.ShouldBe(Aula.MaterialLengthMsgErro);
    }

   
   
}
