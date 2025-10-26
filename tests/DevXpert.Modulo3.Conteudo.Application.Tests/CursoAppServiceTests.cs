using DevXpert.Modulo3.ModuloConteudo.Application.Mapper;
using DevXpert.Modulo3.ModuloConteudo.Application.Services;
using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;
using DevXpert.Modulo3.ModuloConteudo.Domain;
using DevXpert.Modulo3.Core.DomainObjects;
using Moq;
using Shouldly;
using System.Linq.Expressions;
using DevXpert.Modulo3.ModuloConteudo.Application.Tests.Fixture;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using MediatR;

namespace DevXpert.Modulo3.ModuloConteudo.Application.Tests;

[Collection(nameof(CursoCollection))]
[Trait("Teste Unidade", "Services CursoAppService")]
public class CursoAppServiceTests
{
    private readonly CursoFixture _cursoFixture;
    private readonly CursoAppService _cursoService;

    public CursoAppServiceTests(CursoFixture cursoFixture)
    {
        _cursoFixture = cursoFixture;
        _cursoService = _cursoFixture.CreateCursoAppService();
    }

    [Fact(DisplayName = "CursoAppService ObterPorId DeveRetornarCurso")]
    public async Task CursoAppService_ObterPorId_DeveRetornarCurso()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(curso.Id))
                     .ReturnsAsync(curso);

        var result = await _cursoService.ObterPorId(curso.Id);

        result.Id.ShouldBe(curso.Id);
        result.ShouldBeAssignableTo<CursoViewModel>();

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(curso.Id), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService ObterTodos DeveRetornarLista")]
    public async Task CursoAppService_ObterTodos_DeveRetornarLista()
    {
        var lista = new List<Curso>() { new("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo")) };

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.ObterTodos())
                     .ReturnsAsync(lista);

        var result = await _cursoService.ObterTodos();

        result.Count().ShouldBe(lista.Count);

        result.ShouldBeAssignableTo<IEnumerable<CursoViewModel>>();

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.ObterTodos(), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService Adicionar NaoDeveAdicionarCursoJaCadastrado")]
    public async Task CursoAppService_Adicionar_NaoDeveAdicionarCursoJaCadastrado()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var model = CursoMappingProfile.MapCursoToCursoViewModel(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()))
                     .ReturnsAsync([curso]);

        await _cursoService.AdicionarCurso(model);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()), Times.Once);

        _cursoFixture.Mocker.GetMock<IMediatrHandler>()
            .Verify(m => m.PublicarNotificacao(It.Is<DomainNotification>(n => n.Value == "Já existe um curso com este nome cadastrado.")), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService Adicionar DeveAdicionar")]
    public async Task CursoAppService_Adicionar_DeveAdicionar()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var model = CursoMappingProfile.MapCursoToCursoViewModel(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()))
                     .ReturnsAsync(Enumerable.Empty<Curso>);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.UnitOfWork.Commit())
                     .ReturnsAsync(true);

        await _cursoService.AdicionarCurso(model);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService ObterAulaPorId DeveRetornarAula")]
    public async Task CursoAppService_ObterAulaPorId_DeveRetornarAula()
    {
        var aula = new Aula(Guid.NewGuid(), "Mock Conteudo da Aula", "Mock Material da Aula");

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.ObterAula(aula.Id))
                     .ReturnsAsync(aula);

        var result = await _cursoService.ObterAulaPorId(aula.Id);

        result.Id.ShouldBe(aula.Id);
        result.ShouldBeAssignableTo<AulaViewModel>();

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.ObterAula(aula.Id), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService ObterAulas DeveRetornarAulas")]
    public async Task CursoAppService_ObterAulas_DeveRetornarAulas()
    {
        var lista = new List<Aula>() { new(Guid.NewGuid(), "Mock Conteudo da Aula", "Mock Material da Aula") };

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.ObterAulas(lista[0].CursoId))
                     .ReturnsAsync(lista);

        var result = await _cursoService.ObterAulas(lista[0].CursoId);

        result.Count().ShouldBe(lista.Count);

        result.ShouldBeAssignableTo<IEnumerable<AulaViewModel>>();

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.ObterAulas(lista[0].CursoId), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService AdicionarAula NaoDeveAdicionarCursoNaoEncontrado")]
    public async Task CursoAppService_AdicionarAula_NaoDeveAdicionarCursoNaoEncontrado()
    {
        var aula = new Aula(Guid.NewGuid(), "Mock Conteudo da Aula", "Mock Material da Aula");
        var model = CursoMappingProfile.MapAulaToAulaViewModel(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(aula.CursoId))
                     .ReturnsAsync((Curso)null);

        await _cursoService.AdicionarAula(model);

        _cursoFixture.Mocker.GetMock<IMediatrHandler>()
            .Verify(m => m.PublicarNotificacao(It.Is<DomainNotification>(n => n.Value == "Curso não encontrado.")), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Adicionar(It.IsAny<Aula>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService AdicionarAula DeveAdicionar")]
    public async Task CursoAppService_AdicionarAula_DeveAdicionar()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var aula = new Aula(curso.Id, "Mock Conteudo da Aula", "Mock Material da Aula");
        var model = CursoMappingProfile.MapAulaToAulaViewModel(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(curso.Id))
                     .ReturnsAsync(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.UnitOfWork.Commit())
                     .ReturnsAsync(true);

        await _cursoService.AdicionarAula(model);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Adicionar(It.IsAny<Aula>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }
}
