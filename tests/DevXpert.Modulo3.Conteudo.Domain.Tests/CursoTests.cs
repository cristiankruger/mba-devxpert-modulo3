using DevXpert.Modulo3.Conteudo.Domain.Tests._Fixture;
using DevXpert.Modulo3.Core.DomainObjects;
using Shouldly;

namespace DevXpert.Modulo3.Conteudo.Domain.Tests;


[Collection(nameof(CursoCollection))]
[Trait("Teste Unidade", "CursoDomain Curso")]
public class CursoTests(CursoTestsFixture cursoTestsFixture)
{
    private readonly CursoTestsFixture _cursosFixture = cursoTestsFixture;

    [Fact]
    public void Curso_Validar_ObjetoNaoEhEntidade()
    {
        var result = _cursosFixture.GerarCursoValido();

        result.ShouldBeOfType<Curso>();
        result.ShouldBeAssignableTo<Entity>();
        result.ShouldBeAssignableTo<IAggregateRoot>();
    }

    [Fact]
    public void Curso_Validar_ValidacoesNomeDevemRetornarException()
    {
        var ementa = _cursosFixture.GerarEmentaValida();
        var instrutor = _cursosFixture.GerarInstrutorValido();
        var publicoAlvo = _cursosFixture.GerarPublicoAlvoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeCursoInvalido(_cursosFixture.GerarNomeInvalido(true), instrutor, ementa, publicoAlvo)
        );

        Assert.Equal(Curso.NomeVazioMsgErro, ex.Message);
        
        ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeCursoInvalido(_cursosFixture.GerarNomeInvalido(), instrutor, ementa,  publicoAlvo)
        );

        Assert.Equal(Curso.NomeLengthMsgErro, ex.Message);
    }

   
}

