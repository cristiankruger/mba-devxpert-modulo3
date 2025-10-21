using DevXpert.Modulo3.API.Tests.Extensions;
using DevXpert.Modulo3.Core.Application.ViewModels;
using Shouldly;
using System.Net;

namespace DevXpert.Modulo3.API.Tests.AuthController;

[Collection(nameof(AuthCollection))]
[Trait("Integração API", "AuthController")]
[TestCaseOrderer("DevXpert.Modulo3.API.Tests.Extensions.PriorityOrderer", "DevXpert.Modulo3.API.Tests.Extensions")]
public class AuthIntegrationTests(AuthFixture fixture)
{
    private readonly AuthFixture _fixture = fixture;

    [Theory(DisplayName = "AuthController Autenticar DeveAutenticar")]
    [InlineData("admin@teste.com", "@Aa12345")]
    [InlineData("aluno@teste.com", "@Aa12345")]
    public async Task AuthController_Autenticar_DeveAutenticar(string email, string senha)
    {
        var userLogin = new LoginViewModel() { Email = email, Senha = senha };
        var response = await _fixture.PostAutenticarResponse(userLogin);
        var authResponse = await _fixture.DeserializeAuthResultResponseModel(response);

        authResponse.Success.ShouldBeTrue();
        authResponse.Errors.ShouldBeNull();
        authResponse.Data.ShouldNotBeNull();
    }

    [Theory(DisplayName = "AuthController Autenticar NãoDeveAutenticarCredenciaisInvalidas")]
    [InlineData("cristian.silva@uol.com.br", "123#ABc")]
    [InlineData("fulano.ciclano@gmail.com", "321*xyZ")]
    [InlineData("cristian.kruger@live.com", "123$qwE")]
    public async Task AuthController_Autenticar_NaoDeveAutenticarCredenciaisInvalidas(string email, string senha)
    {
        var userLogin = new LoginViewModel() { Email = email, Senha = senha };
        var response = await _fixture.PostAutenticarResponse(userLogin);
        var authResponse = await _fixture.DeserializeAuthResultResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        authResponse.Success.ShouldBeFalse();
        authResponse.Errors.Count().ShouldBeGreaterThan(0);
        authResponse.Errors.Select(e => e).ShouldContain("Usuário ou senha incorretos.");
    }

    [Theory(DisplayName = "AuthController Autenticar NãoDeveAutenticarModelStateInvalido")]
    [InlineData("cristian.silva@uol.com.br", "")]
    [InlineData("fulano.ciclanogmail.com", "321*xyZ")]
    [InlineData("", "123$qwE")]
    public async Task AuthController_Autenticar_NaoDeveAutenticarModelStateInvalido(string email, string senha)
    {
        var userLogin = new LoginViewModel() { Email = email, Senha = senha };
        var response = await _fixture.PostAutenticarResponse(userLogin);
        var authResponse = await _fixture.DeserializeAuthResultResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        authResponse.Success.ShouldBeFalse();
        authResponse.Data.ShouldBeNull();
        authResponse.Errors.Count().ShouldBeGreaterThan(0);
    }

    [Theory(DisplayName = "AuthController Cadastrar DeveRetornarToken"), TestPriority(1)]
    [InlineData(false)]
    [InlineData(true)]
    public async Task AuthController_Cadastrar_DeveRetornarToken(bool ehAluno)
    {
        _fixture.RecreateDatabase();
        var userLogin = _fixture.SetCadastroObject(ehAluno);
        var response = await _fixture.PostCadastrarResponse(userLogin);
        var authResponse = await _fixture.DeserializeAuthResultResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        authResponse.Success.ShouldBeTrue();
        authResponse.Errors.ShouldBeNull();
        authResponse.Data.ShouldNotBeNull();
    }

    [Theory(DisplayName = "AuthController Cadastrar DeveRetornarBadRequest")]
    [InlineData(false)]
    [InlineData(true)]
    public async Task AuthController_Cadastrar_DeveRetornarBadRequest(bool ehAluno)
    {
        var cadastro = _fixture.SetCadastroObject(ehAluno);
        await _fixture.PostCadastrarResponse(cadastro);
        var response = await _fixture.PostCadastrarResponse(cadastro);
        var authResponse = await _fixture.DeserializeAuthResultResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        authResponse.Data.ShouldBeNull();
        authResponse.Success.ShouldBeFalse();
        authResponse.Errors.Count().ShouldBeGreaterThan(0);
        authResponse.Errors.Select(e => e).ShouldContain($"Login '{cadastro.Email}' já está sendo utilizado.");
    }

    [Theory(DisplayName = "AuthController Cadastrar DeveRetornarModelStateInvalido")]
    [InlineData(false)]
    [InlineData(true)]
    public async Task AuthController_Cadastrar_DeveRetornarModelStateInvalido(bool ehAluno)
    {
        var cadastro = _fixture.SetCadastroObject(ehAluno);
        cadastro.Email = "mailinvalido.com";
        cadastro.Senha = string.Empty;

        var response = await _fixture.PostCadastrarResponse(cadastro);
        var authResponse = await _fixture.DeserializeAuthResultResponseModel(response);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        authResponse.Data.ShouldBeNull();
        authResponse.Success.ShouldBeFalse();
        authResponse.Errors.Count().ShouldBeGreaterThan(0);
        authResponse.Errors.Select(e => e).ShouldContain("O campo E-mail deve ser um endereço de e-mail válido.");
        authResponse.Errors.Select(e => e).ShouldContain("O campo Senha é obrigatório.");
        authResponse.Errors.Select(e => e).ShouldContain("As senhas não conferem.");
    }

}
