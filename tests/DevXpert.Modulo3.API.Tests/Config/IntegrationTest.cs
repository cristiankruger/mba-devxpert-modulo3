using DevXpert.Modulo3.API.Tests.AuthController;
using DevXpert.Modulo3.Core.Application.ViewModels;
using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.ModuloConteudo.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevXpert.Modulo3.API.Tests.Config;

public abstract class IntegrationTest : IDisposable
{
    protected HttpClient _client;
    protected AuthResultResponseModel Usuario;
    protected WebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions options;
     

    protected IntegrationTest()
    {
        options = new()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        });

        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true,
            BaseAddress = new Uri("https://localhost"),
            HandleCookies = true,
            MaxAutomaticRedirections = 7
        };

        _client = _factory.CreateClient(clientOptions);
    }

    public virtual void RecreateDatabase()
    {
        using var scope = _factory.Services.CreateScope();
        var services = scope.ServiceProvider;

        var identityContext = services.GetService<IdentityAppContext>();

        identityContext.Database.EnsureDeleted();
        identityContext.Database.Migrate();

        var cursoContext = services.GetService<CursoContext>();

        cursoContext.Database.EnsureDeleted();
        cursoContext.Database.Migrate();
    }

    protected async Task Authenticate(string email = "", string senha = "", bool authenticate = true)
    {
        if (authenticate)
            await Login(email, senha);
        else
            _client.DefaultRequestHeaders.Clear();
    }

    private async Task Login(string email, string senha)
    {
        if (string.IsNullOrEmpty(email)) email = "admin@teste.com";
        if (string.IsNullOrEmpty(senha)) senha = "@Aa12345";

        var userLogin = new LoginViewModel() { Email = email, Senha = senha };

        var response = await _client.PostAsJsonAsync("api/v1/auth", userLogin);
        response.EnsureSuccessStatusCode();
        var usuarioResponse = await response.Content.ReadAsStringAsync();
        Usuario = JsonSerializer.Deserialize<AuthResultResponseModel>(usuarioResponse, options);

        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Usuario.Data);
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
