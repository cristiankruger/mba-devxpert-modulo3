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

    [Fact(DisplayName = "CursoAppService PermitirInscricaoCurso NaoDevePermitirInscricaoCursoNaoEncontrado")]
    public async Task CursoAppService_PermitirInscricaoCurso_NaoDevePermitirInscricaoCursoNaoEncontrado()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(It.IsAny<Guid>()))
                     .ReturnsAsync((Curso)null);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
            await _cursoService.PermitirInscricaoCurso(curso.Id)
       );

        ex.Message.ShouldBe("Curso não encontrado.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService PermitirInscricaoCurso NaoDevePermitirInscricaoCursoSemAula")]
    public async Task CursoAppService_PermitirInscricaoCurso_NaoDevePermitirInscricaoCursoSemAula()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(It.IsAny<Guid>()))
                     .ReturnsAsync(curso);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
            await _cursoService.PermitirInscricaoCurso(curso.Id)
       );

        ex.Message.ShouldBe("Não é possível permitir inscrição em um curso sem aulas.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService PermitirInscricaoCurso DevePermitirInscricaoCurso")]
    public async Task CursoAppService_PermitirInscricaoCurso_DevePermitirInscricaoCurso()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var aula = new Aula(curso.Id, "Mock Aula 1", "http://link.com", TimeSpan.FromMinutes(30));
        curso.CadastrarAula(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(It.IsAny<Guid>()))
                     .ReturnsAsync(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.UnitOfWork.Commit())
                     .ReturnsAsync(true);

        await _cursoService.PermitirInscricaoCurso(curso.Id);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService ProibirInscricaoCurso NaoDeveProibirInscricaoCursoNaoEncontrado")]
    public async Task CursoAppService_ProibirInscricaoCurso_NaoDeveProibirInscricaoCursoNaoEncontrado()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(It.IsAny<Guid>()))
                     .ReturnsAsync((Curso)null);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
            await _cursoService.ProibirInscricaoCurso(curso.Id)
        );

        ex.Message.ShouldBe("Curso não encontrado.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService ProibirInscricaoCurso DeveProibirInscricaoCurso")]
    public async Task CursoAppService_ProibirInscricaoCurso_DeveProibirInscricaoCurso()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(It.IsAny<Guid>()))
                     .ReturnsAsync(curso);

        _cursoFixture.Mocker
                    .GetMock<ICursoRepository>()
                    .Setup(r => r.UnitOfWork.Commit())
                    .ReturnsAsync(true);

        await _cursoService.ProibirInscricaoCurso(curso.Id);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Curso>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }

    [Fact(DisplayName = "CursoAppService ObterAulaPorId DeveRetornarAula")]
    public async Task CursoAppService_ObterAulaPorId_DeveRetornarAula()
    {
        var aula = new Aula(Guid.NewGuid(), "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30));

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
        var lista = new List<Aula>() { new(Guid.NewGuid(), "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30)) };
        
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
        var aula = new Aula(Guid.NewGuid(), "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30));
        var model = CursoMappingProfile.MapAulaToAulaViewModel(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(aula.CursoId))
                     .ReturnsAsync((Curso)null);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
            await _cursoService.AdicionarAula(model)
        );

        ex.Message.ShouldBe("Curso não encontrado.");

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
        var aula = new Aula(curso.Id, "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30));
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

    [Fact(DisplayName = "CursoAppService AtualizarAula NaoDeveAtualizarAulaNaoEncontrada")]
    public async Task CursoAppService_AtualizarAula_NaoDeveAtualizarAulaNaoEncontrada()
    {
        var aula = new Aula(Guid.NewGuid(), "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30));
        var model = CursoMappingProfile.MapAulaToAulaViewModel(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.ObterAula(aula.Id))
                     .ReturnsAsync((Aula)null);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
            await _cursoService.AtualizarAula(model)
        );

        ex.Message.ShouldBe("Aula não encontrada.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.ObterAula(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Adicionar(It.IsAny<Aula>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService AtualizarAula NaoDeveAtualizarAulaCursoNaoEncontrado")]
    public async Task CursoAppService_AtualizarAula_NaoDeveAtualizarAulaCursoNaoEncontrado()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var aula = new Aula(curso.Id, "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30));
        var model = CursoMappingProfile.MapAulaToAulaViewModel(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.ObterAula(aula.Id))
                     .ReturnsAsync(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(aula.CursoId))
                     .ReturnsAsync((Curso)null);

        var ex = await Assert.ThrowsAsync<DomainException>(async () =>
            await _cursoService.AtualizarAula(model)
        );

        ex.Message.ShouldBe("Curso não encontrado.");

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.ObterAula(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Adicionar(It.IsAny<Aula>()), Times.Never);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Never);
    }

    [Fact(DisplayName = "CursoAppService AtualizarAula DeveAtualizarAula")]
    public async Task CursoAppService_AtualizarAula_DeveAtualizarAula()
    {
        var curso = new Curso("Mock Nome", new("Mock Instrutor", "Mock Ementa de 20 caracteres", "Mock Publico Alvo"));
        var aula = new Aula(curso.Id, "Mock Titulo da Aula", "https://youtube.com/meulink", TimeSpan.FromMinutes(30));
        var model = CursoMappingProfile.MapAulaToAulaViewModel(aula);
        curso.CadastrarAula(aula);
        aula.AlterarTitulo("Mock Novo Titulo da Aula");

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.ObterAula(aula.Id))
                     .ReturnsAsync(aula);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.Obter(aula.CursoId))
                     .ReturnsAsync(curso);

        _cursoFixture.Mocker
                     .GetMock<ICursoRepository>()
                     .Setup(r => r.UnitOfWork.Commit())
                     .ReturnsAsync(true);

        await _cursoService.AtualizarAula(model);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.ObterAula(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Obter(It.IsAny<Guid>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.Atualizar(It.IsAny<Aula>()), Times.Once);

        _cursoFixture.Mocker.GetMock<ICursoRepository>()
            .Verify(r => r.UnitOfWork.Commit(), Times.Once);
    }
}
