using DevXpert.Modulo3.Aluno.Data.Tests._Fixture;
using DevXpert.Modulo3.Core.DomainObjects;
using Shouldly;

namespace DevXpert.Modulo3.Aluno.Data.Tests;

[Collection(nameof(AlunoCollection))]
[Trait("Teste Unidade", "AlunoDomain Aluno")]
public class AlunoTests(AlunoTestsFixture alunoTestsFixture)
{
    private readonly AlunoTestsFixture _alunosFixture = alunoTestsFixture;

    [Fact]
    public void Aluno_Validar_ObjetoNaoEhEntidade()
    {
        var result = _alunosFixture.GerarAlunoValido();

        result.ShouldBeOfType<Domain.Aluno>();
        result.ShouldNotBeAssignableTo<Entity>();
        result.ShouldNotBeAssignableTo<IAggregateRoot>();
    }

    [Fact]
    public void Aluno_Validar_ValidacoesNomeDevemRetornarException()
    {
        var email = _alunosFixture.GerarEmailValido();
        var cpf = _alunosFixture.GerarCpfValido();
        var data = _alunosFixture.GerarDataNascimentoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("", email, cpf, data)
        );

        Assert.Equal(Domain.Aluno.NomeVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("Jose 123 @", email, cpf, data)
        );

         Assert.Equal(Domain.Aluno.NomeRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("z e", email, cpf, data)
        );

         Assert.Equal(Domain.Aluno.NomeLenghtMsgErro, ex.Message);
    }

    [Fact]
    public void Aluno_Validar_ValidacoesEmailDevemRetornarException()
    {
        var nome = _alunosFixture.GerarNomeValido();
        var cpf = _alunosFixture.GerarCpfValido();
        var data = _alunosFixture.GerarDataNascimentoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "", cpf, data)
        );

         Assert.Equal(Domain.Aluno.EmailVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "123456", cpf, data)
        );

         Assert.Equal(Domain.Aluno.EmailRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "jo#com.br", cpf, data)
        );

         Assert.Equal(Domain.Aluno.EmailRegexMsgErro, ex.Message);
    }

    [Fact]
    public void Aluno_Validar_ValidacoesCpfDevemRetornarException()
    {
        var nome = _alunosFixture.GerarNomeValido();
        var email = _alunosFixture.GerarEmailValido();
        var data = _alunosFixture.GerarDataNascimentoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, email, "", data)
        );

         Assert.Equal(Domain.Aluno.CpfVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, nome, "1234", data)
        );

         Assert.Equal(Domain.Aluno.CpfLenghtMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, nome, "A1@", data)
        );

         Assert.Equal(Domain.Aluno.CpfRegexMsgErro, ex.Message);
    }

    [Fact]
    public void Aluno_Validar_ValidacoesDataDevemRetornarException()
    {
        var nome = _alunosFixture.GerarNomeValido();
        var email = _alunosFixture.GerarEmailValido();
        var cpf = _alunosFixture.GerarCpfValido();

        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, email, cpf, DateTime.Now.AddYears(-10))
        );

         Assert.Equal(Domain.Aluno.DataNascimentoMsgErro, ex.Message);        
    }
}
