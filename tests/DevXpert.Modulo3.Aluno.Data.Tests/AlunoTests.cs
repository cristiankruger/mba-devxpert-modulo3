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
        var cpf = _alunosFixture.GerarCpfValido();
        var data = _alunosFixture.GerarDataNascimentoValida();

        var ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("", email, cpf, data)
        );

        Assert.Equal(Aluno.NomeVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("Jose 123 @", email, cpf, data)
        );

         Assert.Equal(Aluno.NomeRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido("z e", email, cpf, data)
        );

         Assert.Equal(Aluno.NomeLenghtMsgErro, ex.Message);
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

         Assert.Equal(Aluno.EmailVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "123456", cpf, data)
        );

         Assert.Equal(Aluno.EmailRegexMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, "jo#com.br", cpf, data)
        );

         Assert.Equal(Aluno.EmailRegexMsgErro, ex.Message);
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

         Assert.Equal(Aluno.CpfVazioMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, nome, "1234", data)
        );

         Assert.Equal(Aluno.CpfLenghtMsgErro, ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            _alunosFixture.GerarFakeAlunoInvalido(nome, nome, "12345678A1@", data)
        );

         Assert.Equal(Aluno.CpfRegexMsgErro, ex.Message);
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

         Assert.Equal(Aluno.DataNascimentoMsgErro, ex.Message);        
    }
}
