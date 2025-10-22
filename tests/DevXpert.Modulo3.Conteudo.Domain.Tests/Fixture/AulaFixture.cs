using Bogus;
using DevXpert.Modulo3.ModuloConteudo.Domain;

namespace DevXpert.Modulo3.ModuloConteudo.Domain.Tests.Fixture;

[CollectionDefinition(nameof(AulaCollection))]
public class AulaCollection : ICollectionFixture<AulaFixture>
{ }

public class AulaFixture : IDisposable
{
    public Aula GerarAulaValido()
    {
        return GerarFakeAulaValido(1).FirstOrDefault();
    }

    private static IEnumerable<Aula> GerarFakeAulaValido(int quantidade)
    {
        var aula = new Faker<Aula>("pt_BR")
                                .CustomInstantiator(f =>
                                        new Aula(Guid.NewGuid(),
                                                 string.Join(" ", f.Lorem.Words(5)),
                                                 f.Internet.Url(),
                                                 TimeSpan.FromSeconds(f.Random.Int(1, 7200))));

        var generated = aula.Generate(quantidade);

        return generated;
    }

    public Aula GerarFakeAulaInvalido(Guid cursoId, string titulo, string link, TimeSpan duracao)
    {
        return new Faker<Aula>("pt_BR")
           .CustomInstantiator(f => new Aula(cursoId, titulo, link, duracao));
    }

    public string GerarTituloValido()
    {
        var f = new Faker("pt_BR");
        return string.Join(" ", f.Lorem.Words(3));
    }

    public string GerarTituloInvalido(bool vazio = false)
    {
        if (vazio) return string.Empty;

        var f = new Faker("pt_BR");
        return string.Join(" ", f.Lorem.Words(50));
    }

    public string GerarLinkValido()
    {
        return new Faker().Internet.Url();
    }

    public string GerarLinkInvalido()
    {
        return string.Join(" ", new Faker().Lorem.Words(200));
    }

    public TimeSpan GerarDuracaoValida()
    {
        var f = new Faker("pt_BR");
        return TimeSpan.FromSeconds(f.Random.Int(1, 7200));
    }

    public void Dispose()
    {

    }
}

