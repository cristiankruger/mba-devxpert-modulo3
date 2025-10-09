using Microsoft.AspNetCore.Localization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class GlobalizationConfig
{
    public static WebApplication UseGlobalizationConfig(this WebApplication app)
    {
        var defaultCulture = new CultureInfo("pt-BR");

        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = [defaultCulture],
            SupportedUICultures = [defaultCulture]
        };

        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}
