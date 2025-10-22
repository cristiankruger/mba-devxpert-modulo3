using Bogus;
using DevXpert.Modulo3.ModuloConteudo.Domain;

namespace DevXpert.Modulo3.ModuloConteudo.Domain.Tests.Fixture;

[CollectionDefinition(nameof(CursoCollection))]
public class CursoCollection : ICollectionFixture<CursoFixture>
{ }

public class CursoFixture : IDisposable
{
    public Curso GerarCursoValido()
    {
        return GerarFakeCursoValido(1).FirstOrDefault();
    }

    public ConteudoProgramatico GerarConteudoProgramaticoValido()
    {
        return GerarFakeConteudoProgramaticoValido(1).FirstOrDefault();
    }

    public Aula GerarAulaValido(Guid cursoId)
    {
        return GerarFakeAulaValido(1, cursoId).FirstOrDefault();
    }

    private static IEnumerable<Aula> GerarFakeAulaValido(int quantidade, Guid cursoId)
    {
        var aula = new Faker<Aula>("pt_BR")
                                .CustomInstantiator(f =>
                                        new Aula(cursoId == Guid.Empty ? Guid.NewGuid() : cursoId,
                                                 string.Join(" ", f.Lorem.Words(5)),
                                                 f.Internet.Url(),
                                                 TimeSpan.FromSeconds(f.Random.Int(1, 7200))));

        var generated = aula.Generate(quantidade);

        return generated;
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

    private static IEnumerable<ConteudoProgramatico> GerarFakeConteudoProgramaticoValido(int quantidade)
    {
        var curso = new Faker<ConteudoProgramatico>("pt_BR")
                                .CustomInstantiator(f =>
                                        new ConteudoProgramatico(f.Person.FullName,
                                                                 string.Join(" ", f.Lorem.Words(10)),
                                                                 string.Join(" ", f.Lorem.Words(5))));
        var generated = curso.Generate(quantidade);

        return generated;
    }

    public Curso GerarFakeCursoInvalido(string nome, string ementa, string instrutor, string publicoAlvo)
    {
        return new Faker<Curso>("pt_BR")
           .CustomInstantiator(f => new Curso(nome, new(ementa, instrutor, publicoAlvo)));
    }

    public ConteudoProgramatico GerarFakeConteudoProgramaticoInvalido(string ementa, string instrutor, string publicoAlvo)
    {
        return new Faker<ConteudoProgramatico>("pt_BR")
           .CustomInstantiator(f => new ConteudoProgramatico(ementa, instrutor, publicoAlvo));
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
        return string.Join(" ", f.Lorem.Words(500));
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

