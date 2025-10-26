using DevXpert.Modulo3.Core.DomainObjects;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status403Forbidden ||
                context.Response.StatusCode == StatusCodes.Status401Unauthorized)

                await context.Response.WriteAsync(JsonSerializer.Serialize(SetResultTemplate(context.Response.StatusCode, context.TraceIdentifier), SetOptions()));
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsync(JsonSerializer.Serialize(SetResultTemplate(context.Response.StatusCode, context.TraceIdentifier, [ex.Message]), SetOptions()));
        }
        catch (Exception ex)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await HandleExceptionAsync(context, ex);

                await context.Response.WriteAsync(JsonSerializer.Serialize(SetResultTemplate(context.Response.StatusCode, context.TraceIdentifier), SetOptions()));
            }
            else
            {
                Console.WriteLine($"Erro não tratado: {ex.Message}");
            }
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        ex.Data.Add("Url", $"{context.Request?.Scheme}://{context.Request?.Host}{context.Request?.Path}");
        ex.Data.Add("Request Body", await GetRequestBody(context));
        ex.Data.Add("QueryString", GetQueryString(context));
        ex.Data.Add("Exception Message", ex.Message);

        Console.WriteLine(ex);
        //TODO: LOG TO FILE, DATABASE OR ANY OTHER LOGGING SYSTEM (ELMAH, SERILOG, etc)
    }

    private static async Task<string> GetRequestBody(HttpContext context)
    {
        context.Request.EnableBuffering();
        context.Request.Body.Position = 0;

        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        return string.IsNullOrEmpty(body) ? "Sem RequestBody" : body;
    }

    private static string GetQueryString(HttpContext context)
    {
        var queryString = context.Request?.QueryString.ToString();
        return string.IsNullOrEmpty(queryString) ? "Sem QueryString" : queryString;
    }

    private static JsonSerializerOptions SetOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

    }

    private static StatusObject SetResultTemplate(int status, string traceIdentifier, List<string> errors = null)
    {
        StatusObject statusObject = new();

        switch (status)
        {
            case StatusCodes.Status500InternalServerError:

                statusObject.SetDefaultInternalServerError(traceIdentifier);
                break;

            case StatusCodes.Status401Unauthorized:

                statusObject.SetDefaultUnauthorizedError(traceIdentifier);
                break;

            case StatusCodes.Status403Forbidden:

                statusObject.SetDefaultForbiddenError(traceIdentifier);
                break;

            case StatusCodes.Status400BadRequest:
            default:

                statusObject.SetDefaultBadRequestError(traceIdentifier, errors);
                break;
        }

        return statusObject;
    }
}

public class StatusObject
{
    public int Status { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string TraceId { get; set; }
    public List<string> Errors { get; set; } = [];

    public StatusObject()
    {

    }
    
    public void SetDefaultInternalServerError(string traceIdentifier)
    {
        Status = StatusCodes.Status500InternalServerError;
        TraceId = traceIdentifier;
        Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1";
        Title = "Internal Server Error";
        Detail = "Ocorreu um erro inesperado no servidor.";
        Errors.Add(Detail);
    }

    public void SetDefaultUnauthorizedError(string traceIdentifier)
    {
        Status = StatusCodes.Status401Unauthorized;
        TraceId = traceIdentifier;
        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
        Title = "Unauthorized";
        Detail = "Acesso negado. É necessário autenticação para acessar este recurso.";
        Errors.Add(Detail);
    }

    public void SetDefaultForbiddenError(string traceIdentifier)
    {
        Status = StatusCodes.Status403Forbidden;
        TraceId = traceIdentifier;
        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.4";
        Title = "Forbidden";
        Detail = "Acesso negado. Você não tem permissão para acessar este recurso.";
        Errors.Add(Detail);
    }

    public void SetDefaultBadRequestError(string traceIdentifier, List<string> erros)
    {
        Status = StatusCodes.Status400BadRequest;
        TraceId = traceIdentifier;
        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
        Title = "Bad Request";
        Detail = "Um erro inesperado ocorreu.";
        Errors = [.. erros.Select(s => s)];
    }
}
