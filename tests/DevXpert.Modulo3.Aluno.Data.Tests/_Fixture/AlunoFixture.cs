using Bogus;
using Bogus.Extensions.Brazil;

namespace DevXpert.Modulo3.Aluno.Data.Tests._Fixture;

[CollectionDefinition(nameof(AlunoCollection))]
public class AlunoCollection : ICollectionFixture<AlunoTestsFixture>
{ }

public class AlunoTestsFixture : IDisposable
{
    public Domain.Aluno GerarAlunoValido()
    {
        return GerarFakeAlunoValido(1).FirstOrDefault();
    }

    private static IEnumerable<Domain.Aluno> GerarFakeAlunoValido(int quantidade)
    {
        var modulo = new Faker<Domain.Aluno>("pt_BR")
                                .CustomInstantiator(f =>
                                        new Domain.Aluno($"{f.Person.FirstName} {f.Person.LastName}",
                                                        f.Person.Email,
                                                        f.Person.Cpf(false),
                                                        f.Person.DateOfBirth));

        var generated = modulo.Generate(quantidade);

        return generated;
    }

    public Domain.Aluno GerarFakeAlunoInvalido(string nome, string email, string cpf, DateTime dataNascimento)
    {
        return new Faker<Domain.Aluno>("pt_BR")
           .CustomInstantiator(f => new Domain.Aluno(nome, email, cpf, dataNascimento));
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

    public string GerarCpfValido()
    {
        return new Faker().Person.Cpf(false);
    }

    public DateTime GerarDataNascimentoValida()
    {
        var faker = new Faker();
        return DateTime.Now.AddYears(faker.Random.Int(-18, -70)).AddDays(faker.Random.Int(0, -364));
    }

    public void Dispose()
    {

    }
}
