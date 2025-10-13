using DevXpert.Modulo3.Conteudo.Application.Mapper;
using DevXpert.Modulo3.Conteudo.Application.Services;
using DevXpert.Modulo3.Conteudo.Application.Tests.Fixture;
using DevXpert.Modulo3.Conteudo.Application.ViewModels;
using DevXpert.Modulo3.Conteudo.Domain;
using DevXpert.Modulo3.Core.DomainObjects;
using Moq;
using Shouldly;
using System;
using System.Linq.Expressions;

namespace DevXpert.Modulo3.Conteudo.Application.Tests;

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

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
           await _cursoService.AdicionarCurso(model)
        );

        ex.Message.ShouldBe("Já existe um curso com este nome cadastrado.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()), Times.Once);

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

    [Fact(DisplayName = "CursoAppService Atualizar NaoDeveAtualizarCursoJaCadastrado")]
    public async Task CursoAppService_Atualizar_NaoDeveAtualizarCursoJaCadastrado()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var model = CursoMappingProfile.MapCursoToCursoViewModel(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()))
                     .ReturnsAsync([curso]);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
                await _cursoService.AtualizarCurso(model)
        );

        ex.Message.ShouldBe("Já existe um curso com este nome cadastrado.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService Atualizar DeveAtualizarCurso")]
    public async Task CursoAppService_Atualizar_DeveAtualizarCurso()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var model = CursoMappingProfile.MapCursoToCursoViewModel(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()))
                     .ReturnsAsync([]);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.UnitOfWork.Commit())
                     .ReturnsAsync(true);

        await _cursoService.AtualizarCurso(model);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Buscar(It.IsAny<Expression<Func<Curso, bool>>>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }
}
