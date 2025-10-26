using DevXpert.Modulo3.API.Tests.Extensions;
using Shouldly;
using System.Net;

namespace DevXpert.Modulo3.API.Tests.CursoController;

[Collection(nameof(CursoCollection))]
[Trait("Integração API", "CursoController")]
[TestCaseOrderer("DevXpert.Modulo3.API.Tests.Extensions.PriorityOrderer", "DevXpert.Modulo3.API.Tests.Extensions")]
public class CursoControllerIntegrationsTests(CursoFixture fixture)
{
    private readonly CursoFixture _fixture = fixture;

    [Fact(DisplayName = "CursoController Adicionar DeveRetornarHttpStatusCode200"), TestPriority(1)]
    public async Task CursoController_Adicionar_DeveRetornarHttpStatusCode200()
    {
        _fixture.RecreateDatabase();
        var curso = _fixture.SetCursoObject();
        var response = await _fixture.AdicionarCursoResponse(curso);
        var responseData = await _fixture.DeserializeCursoResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        responseData.Data.ShouldNotBeNull();
        responseData.Success.ShouldBeTrue();
        responseData.Errors.ShouldBeNull();
    }

    [Fact(DisplayName = "CursoController ObterCursos DeveRetornarHttpStatusCode200"), TestPriority(2)]
    public async Task CursoController_ObterCursos_DeveRetornarHttpStatusCode200()
    {
        var response = await _fixture.ObterCursosResponse();
        var deserializedResponse = await _fixture.DeserializeCursosResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        deserializedResponse.Success.ShouldBeTrue();
        deserializedResponse.Errors.ShouldBeNull();
        deserializedResponse.Data.ShouldNotBeNull();
        deserializedResponse.Data.Count().ShouldBeGreaterThan(0);
    }

    [Fact(DisplayName = "CursoController ObterCurso DeveRetornarHttpStatusCode200"), TestPriority(3)]
    public async Task CursoController_ObterCurso_DeveRetornarHttpStatusCode200()
    {
        var curso = _fixture.SetCursoObject();
        var response = await _fixture.ObterCursoResponse(curso.Id);
        var deserializedResponse = await _fixture.DeserializeCursoResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        deserializedResponse.Success.ShouldBeTrue();
        deserializedResponse.Errors.ShouldBeNull();
        deserializedResponse.Data.ShouldNotBeNull();
        deserializedResponse.Data.Id.ShouldBe(curso.Id);
    }

    [Fact(DisplayName = "CursoController AdicionarAula DeveRetornarHttpStatusCode200"), TestPriority(4)]
    public async Task CursoController_AdicionarAula_DeveRetornarHttpStatusCode200()
    {
        var aula = _fixture.SetAulaObject();
        var response = await _fixture.AdicionarAulaResponse(aula);
        var deserializedResponse = await _fixture.DeserializeAulaResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        deserializedResponse.Success.ShouldBeTrue();
        deserializedResponse.Errors.ShouldBeNull();
        deserializedResponse.Data.ShouldNotBeNull();
        deserializedResponse.Data.Id.ShouldBe(aula.Id);
    }

    [Fact(DisplayName = "CursoController ObterAulas DeveRetornarHttpStatusCode200"), TestPriority(5)]
    public async Task CursoController_ObterAulas_DeveRetornarHttpStatusCode200()
    {
        var aula = _fixture.SetAulaObject();
        var response = await _fixture.ObterAulasResponse(aula.CursoId);
        var deserializedResponse = await _fixture.DeserializeAulasResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        deserializedResponse.Success.ShouldBeTrue();
        deserializedResponse.Errors.ShouldBeNull();
        deserializedResponse.Data.ShouldNotBeNull();
        deserializedResponse.Data.Count().ShouldBeGreaterThan(0);
    }

    [Fact(DisplayName = "CursoController ObterAula DeveRetornarHttpStatusCode200"), TestPriority(6)]
    public async Task CursoController_ObterAula_DeveRetornarHttpStatusCode200()
    {
        var aula = _fixture.SetAulaObject();
        var response = await _fixture.ObterAulaResponse(aula.Id);
        var deserializedResponse = await _fixture.DeserializeAulaResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        deserializedResponse.Success.ShouldBeTrue();
        deserializedResponse.Errors.ShouldBeNull();
        deserializedResponse.Data.ShouldNotBeNull();
        deserializedResponse.Data.Id.ShouldBe(aula.Id);
    }

    //404 INICIO
    [Fact(DisplayName = "CursoController ObterCurso DeveRetornarHttpStatusCode404")]
    public async Task CursoController_ObterCurso_DeveRetornarHttpStatusCode404()
    {
        var response = await _fixture.ObterCursoResponse(Guid.NewGuid());

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = "CursoController ObterAula DeveRetornarHttpStatusCode404")]
    public async Task CursoController_ObterAula_DeveRetornarHttpStatusCode404()
    {
        var response = await _fixture.ObterAulaResponse(Guid.NewGuid());

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    //404 FIM

    //401 INICIO
    [Fact(DisplayName = "CursoController ObterCursos DeveRetornarHttpStatusCode401")]
    public async Task CursoController_ObterCursos_DeveRetornarHttpStatusCode401()
    {
        var response = await _fixture.ObterCursosResponse(false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "CursoController ObterCurso DeveRetornarHttpStatusCode401")]
    public async Task CursoController_ObterCurso_DeveRetornarHttpStatusCode401()
    {
        var response = await _fixture.ObterCursoResponse(Guid.NewGuid(), false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "CursoController AdicionarCurso DeveRetornarHttpStatusCode401")]
    public async Task CursoController_AdicionarCurso_DeveRetornarHttpStatusCode401()
    {
        var curso = _fixture.SetCursoObject();
        var response = await _fixture.AdicionarCursoResponse(curso, false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "CursoController ObterAulas DeveRetornarHttpStatusCode401")]
    public async Task CursoController_ObterAulas_DeveRetornarHttpStatusCode401()
    {
        var response = await _fixture.ObterAulasResponse(Guid.NewGuid(), false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "CursoController ObterAula DeveRetornarHttpStatusCode401")]
    public async Task CursoController_ObterAula_DeveRetornarHttpStatusCode401()
    {
        var response = await _fixture.ObterAulaResponse(Guid.NewGuid(), false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "CursoController AdicionarAula DeveRetornarHttpStatusCode401")]
    public async Task CursoController_AdicionarAula_DeveRetornarHttpStatusCode401()
    {
        var aula = _fixture.SetAulaObject();
        var response = await _fixture.AdicionarAulaResponse(aula, false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    //401 FIM

    //403 INICIO
    [Fact(DisplayName = "CursoController AdicionarAula DeveRetornarHttpStatusCode403")]
    public async Task CursoController_AdicionarAula_DeveRetornarHttpStatusCode403()
    {
        var aula = _fixture.SetAulaObject();
        var response = await _fixture.AdicionarAulaResponse(aula, true, "aluno@teste.com");

        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }

    [Fact(DisplayName = "CursoController AdicionarCurso DeveRetornarHttpStatusCode403")]
    public async Task CursoController_AdicionarCurso_DeveRetornarHttpStatusCode403()
    {
        var curso = _fixture.SetCursoObject();
        var response = await _fixture.AdicionarCursoResponse(curso, true, "aluno@teste.com");

        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }
    //403 FIM
}
