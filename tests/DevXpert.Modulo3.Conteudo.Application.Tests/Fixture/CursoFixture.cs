using DevXpert.Modulo3.ModuloConteudo.Application.Services;
using Moq.AutoMock;

namespace DevXpert.Modulo3.ModuloConteudo.Application.Tests.Fixture;


[CollectionDefinition(nameof(CursoCollection))]
public class CursoCollection : ICollectionFixture<CursoFixture>
{ }

public class CursoFixture
{
    public AutoMocker Mocker;
    public CursoAppService CursoAppService;

    public CursoAppService CreateCursoAppService()
    {
        Mocker = new AutoMocker();
        CursoAppService = Mocker.CreateInstance<CursoAppService>();

        return CursoAppService;
    }
}
