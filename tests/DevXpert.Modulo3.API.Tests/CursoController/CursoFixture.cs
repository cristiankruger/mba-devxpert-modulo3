using DevXpert.Modulo3.API.Tests.Config;
using DevXpert.Modulo3.API.Tests.CursoController.ResponseModel;
using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevXpert.Modulo3.API.Tests.CursoController;

[CollectionDefinition(nameof(CursoCollection))]
public class CursoCollection : ICollectionFixture<CursoFixture>
{ }

public class CursoFixture : IntegrationTest
{
    private readonly string ControllerRoute = "api/v1/curso";
    private readonly JsonSerializerOptions options;

    public CursoFixture()
    {
        options = new()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<HttpResponseMessage> GetObterTodosResponse(bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.GetAsync($"{ControllerRoute}");
    }

    public async Task<CursoObterTodosResponseModel> DeserializeObterTodosResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CursoObterTodosResponseModel>(objResponse, options);
    }

    public async Task<HttpResponseMessage> GetObterPorIdResponse(Guid id, bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.GetAsync($"{ControllerRoute}/{id}");
    }

    public async Task<CursoObterPorIdResponseModel> DeserializeObterPorIdResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CursoObterPorIdResponseModel>(objResponse, options);
    }

    public CursoViewModel SetCursoObject()
    {
        return new CursoViewModel()
        {
            Nome = "Curso de Teste",
            Ementa = "Uma ementa que seja o suficiente",
            Instrutor = "Instrutor Fulano da Silva",
            PublicoAlvo = "Desenvolvedores Juniors, plenos e seniors"
        };
    }
}