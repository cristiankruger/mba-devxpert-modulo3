using DevXpert.Modulo3.Conteudo.Domain.Tests._Fixture;
using DevXpert.Modulo3.Core.DomainObjects;
using Shouldly;

namespace DevXpert.Modulo3.Conteudo.Domain.Tests;

[Collection(nameof(CursoCollection))]
[Trait("Teste Unidade", "ConteudoDomain Curso")]
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

        ex.Message.ShouldBe(Curso.NomeVazioMsgErro);
        
        ex = Assert.Throws<DomainException>(() =>
            _cursosFixture.GerarFakeCursoInvalido(_cursosFixture.GerarNomeInvalido(), instrutor, ementa,  publicoAlvo)
        );

        ex.Message.ShouldBe(Curso.NomeLengthMsgErro);
    }

    [Fact]
    public void Curso_Validar_MetodoAtivarDeveAtivar()
    {
        var curso = _cursosFixture.GerarCursoValido();

        curso.AtivarCurso();

        curso.Ativo.ShouldBeTrue();
    }

    [Fact]
    public void Curso_Validar_MetodoDesativarDeveDesativar()
    {
        var curso = _cursosFixture.GerarCursoValido();

        curso.DesativarCurso();

        curso.Ativo.ShouldBeFalse();
    }

    [Fact]
    public void Curso_Validar_MetodoPermitirInscricaoDevePermitirMatricula()
    {
        var curso = _cursosFixture.GerarCursoValido();

        curso.PermitirInscricao();

        curso.PermitirMatricula.ShouldBeTrue();
    }

    [Fact]
    public void Curso_Validar_MetodoProibirInscricaoNaoDevePermitirMatricula()
    {
        var curso = _cursosFixture.GerarCursoValido();

        curso.ProibirInscricao();

        curso.PermitirMatricula.ShouldBeFalse();
    }

    [Fact]
    public void Curso_Validar_MetodoCadastrarAulaDeveIncluirAulaNaLista()
    {
        var curso = _cursosFixture.GerarCursoValido();
        var aula = _cursosFixture.GerarAulaValido(curso.Id);

        curso.CadastrarAula(aula);

        curso.Aulas.ShouldContain(aula);    
        curso.CargaHoraria.ShouldBe(aula.Duracao);
    }
}

