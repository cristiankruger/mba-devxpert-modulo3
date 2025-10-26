using Bogus;
using Bogus.Extensions.Brazil;

namespace DevXpert.Modulo3.ModuloAluno.Domain.Tests._Fixture;

[CollectionDefinition(nameof(AlunoCollection))]
public class AlunoCollection : ICollectionFixture<AlunoTestsFixture>
{ }

public class AlunoTestsFixture : IDisposable
{
    public Aluno GerarAlunoValido()
    {
        return GerarFakeAlunoValido(1).FirstOrDefault();
    }

    private static IEnumerable<Aluno> GerarFakeAlunoValido(int quantidade)
    {
        var alunos = new Faker<Aluno>("pt_BR")
                                .CustomInstantiator(f =>
                                        new Aluno($"{f.Person.FirstName} {f.Person.LastName}",
                                                  f.Person.Email));

        var generated = alunos.Generate(quantidade);

        return generated;
    }

    public Aluno GerarFakeAlunoInvalido(string nome, string email)
    {
        return new Faker<Aluno>("pt_BR")
           .CustomInstantiator(f => new Aluno(nome, email));
    }

    public string GerarNomeValido()
    {
        var f = new Faker("pt_BR");
        return $"{f.Person.FirstName} {f.Person.LastName}";
    }

    public string GerarEmailValido()
    {
        return new Faker().Person.Email;
    }  

    public void Dispose()
    {

    }
}
