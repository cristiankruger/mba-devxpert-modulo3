using Bogus;

namespace DevXpert.Modulo3.Financeiro.Domain.Tests._Fixture;

[CollectionDefinition(nameof(DadosCartaoCollection))]
public class DadosCartaoCollection : ICollectionFixture<DadosCartaoTestsFixture>
{ }

public class DadosCartaoTestsFixture : IDisposable
{
    public DadosCartao GerarDadosCartaoValido()
    {
        return GerarFakeDadosCartaoValido(1).FirstOrDefault();
    }

    private static IEnumerable<DadosCartao> GerarFakeDadosCartaoValido(int quantidade)
    {
        var modulo = new Faker<DadosCartao>("pt_BR")
                                .CustomInstantiator(f =>
                                        new DadosCartao($"{f.Person.FirstName} {f.Person.LastName}",
                                                        f.Finance.CreditCardNumber().Replace("-", string.Empty),
                                                        f.Finance.CreditCardCvv(),
                                                        $"{DateTime.Now.AddMonths(f.Random.Int(0, 36)):MM/yyyy}"));

        var generated = modulo.Generate(quantidade);

        return generated;
    }

    public DadosCartao GerarFakeDadosCartaoInvalido(string titular, string numeroCartao, string cvv, string data)
    {
        return new Faker<DadosCartao>("pt_BR")
           .CustomInstantiator(f => new DadosCartao(titular, numeroCartao, cvv, data));
    }

    public string GerarNumeroCartaoValido()
    {
        return new Faker().Finance.CreditCardNumber().Replace("-", string.Empty);
    }

    public string GerarTitularValido()
    {
        var f = new Faker();
        return $"{f.Person.FirstName} {f.Person.LastName}";
    }

    public string GerarCvvValido()
    {
        return new Faker().Finance.CreditCardCvv();
    }

    public string GerarDataValidadeValida()
    {
        var faker = new Faker();
        return $"{DateTime.Now.AddMonths(faker.Random.Int(0,36)):MM/yyyy}";
    }

    public void Dispose()
    {
        
    }
}
