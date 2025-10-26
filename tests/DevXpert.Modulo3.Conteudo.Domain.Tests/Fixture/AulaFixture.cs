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
                                                 string.Join(" ", f.Lorem.Words(3)),
                                                 string.Join(" ", f.Lorem.Words(3))
                                                 ));

        var generated = aula.Generate(quantidade);

        return generated;
    }

    public Aula GerarFakeAulaInvalido(Guid cursoId, string conteudo, string material)
    {
        return new Faker<Aula>("pt_BR")
           .CustomInstantiator(f => new Aula(cursoId, conteudo, material));
    }

    public string GerarConteudoValido()
    {
        var f = new Faker("pt_BR");
        return string.Join(" ", f.Lorem.Words(3));
    }

    public string GerarConteudoInvalido(bool vazio = false)
    {
        if (vazio) return string.Empty;

        var f = new Faker("pt_BR");
        return string.Join(" ", f.Lorem.Words(50));
    }

    public string GerarMaterialValido()
    {
        return GerarConteudoValido();
    }

    public string GerarMaterialInvalido()
    {
        return GerarConteudoInvalido();
    }
    
    public void Dispose()
    {

    }
}

