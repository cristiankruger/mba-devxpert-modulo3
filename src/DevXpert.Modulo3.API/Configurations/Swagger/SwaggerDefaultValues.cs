using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Modulo3.API.Configurations.Swagger;

[ExcludeFromCodeCoverage]
public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters is null)
            return;

        foreach (var parameter in operation.Parameters)
        {
            var description = context.ApiDescription
                .ParameterDescriptions
                .First(p => p.Name == parameter.Name);

            var routeInfo = description.RouteInfo;

            operation.Deprecated = OpenApiOperation.DeprecatedDefault;

            parameter.Description ??= description.ModelMetadata?.Description;

            if (routeInfo is null)
                continue;

            if (parameter.In != ParameterLocation.Path && parameter.Schema.Default is null)
                parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());

            parameter.Required |= !routeInfo.IsOptional;
        }
    }
}
