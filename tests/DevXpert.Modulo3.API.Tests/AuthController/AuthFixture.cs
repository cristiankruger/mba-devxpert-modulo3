using DevXpert.Modulo3.API.Tests.AuthController.ResponseModel;
using DevXpert.Modulo3.API.Tests.Config;
using DevXpert.Modulo3.Core.Application.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevXpert.Modulo3.API.Tests.AuthController;

[CollectionDefinition(nameof(AuthCollection))]
public class AuthCollection : ICollectionFixture<AuthFixture>
{ }

public class AuthFixture : IntegrationTest
{
    private readonly string ControllerRoute = "api/v1/auth";
    private readonly JsonSerializerOptions options;

    public AuthFixture()
    {
        options = new()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }   

    public async Task<HttpResponseMessage> PostAutenticarResponse(LoginViewModel user)
    {
        return await _client.PostAsJsonAsync($"{ControllerRoute}", user);
    }

    public async Task<HttpResponseMessage> PostCadastrarResponse(CadastroViewModel user)
    {
        return await _client.PostAsJsonAsync($"{ControllerRoute}/cadastrar", user);
    }

    public async Task<AuthResultResponseModel> DeserializeAuthResultResponseModel(HttpResponseMessage response)
    {
        var objResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuthResultResponseModel>(objResponse, options);
    }

    public CadastroViewModel SetCadastroObject(bool ehAluno = false)
    {
        var role = ehAluno ? "aluno" : "admin";
        return new CadastroViewModel()
        {
            Email = $"teste.{role}@teste.com",
            Senha = "@Aa12345",
            SenhaConfirmacao = "@Aa12345",
            EhAluno = ehAluno
        };
    }
}