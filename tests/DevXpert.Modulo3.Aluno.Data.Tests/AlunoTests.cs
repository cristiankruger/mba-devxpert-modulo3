using DevXpert.Modulo3.ModuloAluno.Domain.Tests._Fixture;
using DevXpert.Modulo3.Core.DomainObjects;
using Shouldly;

namespace DevXpert.Modulo3.ModuloAluno.Domain.Tests;

[Collection(nameof(AlunoCollection))]
[Trait("Teste Unidade", "AlunoDomain Aluno")]
public class AlunoTests(AlunoTestsFixture alunoTestsFixture)
{
    private readonly AlunoTestsFixture _alunosFixture = alunoTestsFixture;

    [Fact]
    public void Aluno_Validar_ObjetoNaoEhEntidade()
    {
        var result = _alunosFixture.GerarAlunoValido();

        result.ShouldBeOfType<Aluno>();
        result.ShouldBeAssignableTo<Entity>();
        result.ShouldBeAssignableTo<IAggregateRoot>();
    }

    [Fact]
    public void Aluno_Validar_ValidacoesNomeDevemRetornarException()
    {
        var email = _alunosFixture.GerarEmailValido();
      
        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("", email)
        );

        Assert.Equal(Aluno.NomeVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("Jose 123 @", email)
        );

         Assert.Equal(Aluno.NomeRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("z e", email)
        );

         Assert.Equal(Aluno.NomeLenghtMsgErro, ex.Message);
    }

    [Fact]
    public void Aluno_Validar_ValidacoesEmailDevemRetornarException()
    {
        var nome = _alunosFixture.GerarNomeValido();
      

        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "")
        );

         Assert.Equal(Aluno.EmailVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "123456")
        );

         Assert.Equal(Aluno.EmailRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "jo#com.br")
        );

         Assert.Equal(Aluno.EmailRegexMsgErro, ex.Message);
    }

    
}
