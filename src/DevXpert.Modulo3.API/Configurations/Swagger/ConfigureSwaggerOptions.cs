using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Modulo3.API.Configurations.Swagger;

[ExcludeFromCodeCoverage]
public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        }
    }

    static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "DevXpert Módulo 3 | Modulo 3 API",
            Description = "Cristian Kruger - Plataforma DevXpert Módulo 3 API.",
            Version = VersionInfo.GetVersionInfo(),
            Contact = new OpenApiContact() { Name = "Cristian Kruger", Email = "cristian.kruger@live.com" }
        };

        if (description.IsDeprecated)
            info.Description += " Esta versão está obsoleta!";

        return info;
    }
}