using Bogus;
using System.Linq;

namespace DevXpert.Modulo3.Conteudo.Domain.Tests._Fixture;

[CollectionDefinition(nameof(CursoCollection))]
public class CursoCollection : ICollectionFixture<CursoTestsFixture>
{ }

public class CursoTestsFixture : IDisposable
{
    public Curso GerarCursoValido()
    {
        return GerarFakeCursoValido(1).FirstOrDefault();
    }

    private static IEnumerable<Curso> GerarFakeCursoValido(int quantidade)
    {
        var curso = new Faker<Curso>("pt_BR")
                                .CustomInstantiator(f =>
                                        new Curso(f.Lorem.Slug(),
                                                  new(f.Person.FullName,
                                                      string.Join(" ", f.Lorem.Words(10)),
                                                      string.Join(" ", f.Lorem.Words(5)))));

        var generated = curso.Generate(quantidade);

        return generated;
    }

    public Curso GerarFakeCursoInvalido(string nome, string ementa, string instrutor, string publicoAlvo)
    {
        return new Faker<Curso>("pt_BR")
           .CustomInstantiator(f => new Curso(nome, new(ementa, instrutor, publicoAlvo)));
    }

    public string GerarNomeValido()
    {
        var f = new Faker("pt_BR");
        return f.Lorem.Slug();
    }

    public string GerarNomeInvalido(bool vazio = false)
    {
        if (vazio) return string.Empty;

        var f = new Faker("pt_BR");
        return string.Join (" ", f.Lorem.Words(50));
    }

    public string GerarEmentaValida()
    {
        return string.Join(" ", new Faker().Lorem.Words(10));
    }

    public string GerarInstrutorValido()
    {
        return new Faker("pt_BR").Person.FullName;
    }

    public string GerarPublicoAlvoValida()
    {
        var faker = new Faker();
        return string.Join(" ", new Faker().Lorem.Words(5));
    }

    public void Dispose()
    {

    }
}

