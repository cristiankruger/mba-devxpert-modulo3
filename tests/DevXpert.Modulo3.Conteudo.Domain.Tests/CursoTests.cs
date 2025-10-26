using DevXpert.Modulo3.Core.DomainObjects;
using DevXpert.Modulo3.ModuloConteudo.Domain.Tests.Fixture;
using Shouldly;

namespace DevXpert.Modulo3.ModuloConteudo.Domain.Tests;

[Collection(nameof(CursoCollection))]
[Trait("Teste Unidade", "ConteudoDomain Curso")]
public class CursoTests(CursoFixture cursoFixture)
{
    private readonly CursoFixture _cursosFixture = cursoFixture;

    [Fact(DisplayName = "Curso Validar ObjetoNaoEhEntidade")]
    public void Curso_Validar_ObjetoNaoEhEntidade()
    {
        var result = _cursosFixture.GerarCursoValido();

        result.ShouldBeOfType<Curso>();
        result.ShouldBeAssignableTo<Entity>();
        result.ShouldBeAssignableTo<IAggregateRoot>();
    }

    [Fact(DisplayName = "Curso Validar ValidacoesNomeDevemRetornarException")]
    public void Curso_Validar_ValidacoesNomeDevemRetornarException()
    {
        var ementa = _cursosFixture.GerarEmentaValida();
        var instrutor = _cursosFixture.GerarInstrutorValido();
        var publicoAlvo = _cursosFixture.GerarPublicoAlvoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeCursoInvalido(_cursosFixture.GerarNomeInvalido(true), instrutor, ementa, publicoAlvo)
        );

        ex.Message.ShouldBe(Curso.NomeVazioMsgErro);
        
        ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeCursoInvalido(_cursosFixture.GerarNomeInvalido(), instrutor, ementa,  publicoAlvo)
        );

        ex.Message.ShouldBe(Curso.NomeLengthMsgErro);
    }

    [Fact(DisplayName = "Curso Validar MetodoAtivarDeveAtivar")]
    public void Curso_Validar_MetodoAtivarDeveAtivar()
    {
        var curso = _cursosFixture.GerarCursoValido();

        curso.AtivarCurso();

        curso.Ativo.ShouldBeTrue();
    }

    [Fact(DisplayName = "Curso Validar MetodoDesativarDeveDesativar")]
    public void Curso_Validar_MetodoDesativarDeveDesativar()
    {
        var curso = _cursosFixture.GerarCursoValido();

        curso.DesativarCurso();

        curso.Ativo.ShouldBeFalse();
    }    

    [Fact(DisplayName = "Curso Validar MetodoCadastrarAulaDeveIncluirAula")]
    public void Curso_Validar_MetodoCadastrarAulaDeveIncluirAula()
    {
        var curso = _cursosFixture.GerarCursoValido();
        var aula = _cursosFixture.GerarAulaValido(curso.Id);

        curso.CadastrarAula(aula);

        curso.Aulas.ShouldContain(aula);    
    }
}

