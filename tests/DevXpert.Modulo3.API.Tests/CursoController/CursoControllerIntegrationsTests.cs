using Shouldly;
using System.Net;

namespace DevXpert.Modulo3.API.Tests.CursoController;

[Collection(nameof(CursoCollection))]
[Trait("Integração API", "CursoController")]
[TestCaseOrderer("DevXpert.Modulo3.API.Tests.Extensions.PriorityOrderer", "DevXpert.Modulo3.API.Tests.Extensions")]
public class CursoControllerIntegrationsTests(CursoFixture fixture)
{
    private readonly CursoFixture _fixture = fixture;

    [Fact(DisplayName = "CursoController ObterTodos DeveRetornarHttpStatusCode401")]
    public async Task CursoController_ObterTodos_DeveRetornarHttpStatusCode401()
    {
        var response = await _fixture.GetObterTodosResponse(false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact(DisplayName = "CursoController ObterPorId DeveRetornarHttpStatusCode401")]
    public async Task CursoController_ObterPorId_DeveRetornarHttpStatusCode401()
    {
        var response = await _fixture.GetObterPorIdResponse(Guid.NewGuid(), false);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
