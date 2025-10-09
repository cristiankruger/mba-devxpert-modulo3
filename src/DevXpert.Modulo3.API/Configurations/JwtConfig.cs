using DevXpert.Modulo3.API.Configurations.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class JwtConfig
{
    public static WebApplicationBuilder AddJWTConfiguration(this WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.GetSection(JwtSettings.ConfigName).Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Jwt);

        builder.Services
               .AddAuthentication(t =>
               {
                   t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(t =>
               {
                   t.RequireHttpsMetadata = true;
                   t.SaveToken = true;
                   t.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudiences = appSettings.ValidoEm,
                       ValidIssuer = appSettings.Emissor,
                       ClockSkew = TimeSpan.Zero
                   };
               });

        return builder;
    }
}
