using DevXpert.Modulo3.Core.Application.ViewModels.Settings;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class SettingsConfig
{
    public static WebApplicationBuilder AddSettingsConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.ConfigName));

        return builder;
    }
}
