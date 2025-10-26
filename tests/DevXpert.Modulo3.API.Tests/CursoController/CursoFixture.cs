using DevXpert.Modulo3.API.Tests.Config;
using DevXpert.Modulo3.API.Tests.CursoController.ResponseModel;
using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;
using System.Net.Http.Json;
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

    //REQUESTS
    public async Task<HttpResponseMessage> AdicionarCursoResponse(CursoViewModel curso, bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.PostAsJsonAsync($"{ControllerRoute}", curso);
    }

    public async Task<HttpResponseMessage> ObterCursosResponse(bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.GetAsync($"{ControllerRoute}");
    }

    public async Task<HttpResponseMessage> ObterCursoResponse(Guid id, bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.GetAsync($"{ControllerRoute}/{id}");
    }

    public async Task<HttpResponseMessage> AdicionarAulaResponse(AulaViewModel aula, bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.PostAsJsonAsync($"{ControllerRoute}/aula", aula);
    }

    public async Task<HttpResponseMessage> ObterAulasResponse(Guid cursoId, bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.GetAsync($"{ControllerRoute}/aulas/{cursoId}");
    }

    public async Task<HttpResponseMessage> ObterAulaResponse(Guid id, bool autenticar = true, string email = "", string senha = "")
    {
        await base.Authenticate(email, senha, autenticar);

        return await _client.GetAsync($"{ControllerRoute}/aulas/id/{id}");
    }

    //DESERIALIZERS
    public async Task<CursosResponseModel> DeserializeCursosResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CursosResponseModel>(objResponse, options);
    }

    public async Task<CursoResponseModel> DeserializeCursoResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CursoResponseModel>(objResponse, options);
    }

    public async Task<AulasResponseModel> DeserializeAulasResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AulasResponseModel>(objResponse, options);
    }

    public async Task<AulaResponseModel> DeserializeAulaResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AulaResponseModel>(objResponse, options);
    }

    //MOCKS
    public CursoViewModel SetCursoObject()
    {
        return new CursoViewModel()
        {
            Id = Guid.Parse("67605008-87cf-4b9f-a7eb-39dc84e8c25a"),
            Nome = "Curso de Teste",
            Ementa = "Uma ementa que seja o suficiente",
            Instrutor = "Instrutor Fulano da Silva",
            PublicoAlvo = "Desenvolvedores Juniors, plenos e seniors"
        };
    }

    public AulaViewModel SetAulaObject()
    {
        return new AulaViewModel()
        {
            Id = Guid.Parse("5ae532e7-a652-4257-8206-fef1a44a3b8b"),
            CursoId = Guid.Parse("67605008-87cf-4b9f-a7eb-39dc84e8c25a"),
            Conteudo = "Conteudo da Aula",
            Material = "Material da Aula"
        };
    }
}