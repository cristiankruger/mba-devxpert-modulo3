using Asp.Versioning;
using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.Core.Mediator;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class ApiConfig
{
    public static WebApplicationBuilder AddApiBehaviorConfig(this WebApplicationBuilder builder)
    {
        builder.Configuration
               .SetBasePath(builder.Environment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
               .AddEnvironmentVariables();

        builder.Services
               .AddControllers()
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.SuppressModelStateInvalidFilter = true;
               });

        return builder;
    }

    public static WebApplicationBuilder ApiVersioningConfig(this WebApplicationBuilder builder)
    {
        builder.Services
              .AddApiVersioning(options =>
              {
                  options.ReportApiVersions = true;
                  options.AssumeDefaultVersionWhenUnspecified = true;
                  options.DefaultApiVersion = new ApiVersion(1, 0);
                  options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                      new HeaderApiVersionReader("x-api-version"),
                                                                      new MediaTypeApiVersionReader("x-api-version"));
              })
              .AddApiExplorer(options =>
              {
                  options.GroupNameFormat = "'v'VVV";
                  options.SubstituteApiVersionInUrl = true;
              });

        return builder;
    }

    public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Default", builder => builder.AllowAnyOrigin()
                                                           .AllowAnyMethod()
                                                           .AllowAnyHeader());

            options.AddPolicy("Production", builder => builder.AllowAnyMethod()
                                                              .WithOrigins("https://DevXpert.com/")
                                                              .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                              .AllowAnyHeader());
        });

        return builder;
    }

    public static WebApplicationBuilder ConfigureMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkyNTQwODAwIiwiaWF0IjoiMTc2MTA3MTEzMCIsImFjY291bnRfaWQiOiIwMTlhMDgwNDNlZTU3MWY1OTQ4YWFjN2QwY2E3NzdjNyIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazg0MDlzc2FxdGhuajRrczVzczY3NWNhIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.KhYbobZgzNT11u6sBtiJGRVAV35SaZjb5u2RA_OzDehnEMj5x611Z0Fyd02QPEY5DP4cSADKd6jr2hfIqwz99o8fOjlESg8vkPI_CUzr4qUnUhgztlTZAOryZMQerne3FY5K894xblmbNtl1ithrm5D2hg4ZQ-w5Az34In1NPGKOe_BQGfa2MUyEo3atqyhkgQwZNir9iuu-qPftJWzZ3qbpEYEzzyjsznXeq7gypc3vL1hI7uJ4gtno3xKzRBnweQM-vMnu_xzH5Zl-BwamXmdcUgSzZDnkLqiRaMuPmejGA8V44ooWHhQwHfFlxXxgs_JLS5VaCtXVYKV8J6n_9Q";
            cfg.RegisterServicesFromAssembly(typeof(MediatrHandler).Assembly);
        });

        return builder;
    }

    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services
               .AddIdentity<IdentityUser, IdentityRole>()
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<IdentityAppContext>()
               .AddErrorDescriber<IdentityErrorsConfig>();

        return builder;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });


        if (!app.Environment.IsDevelopment())
        {
            app.UseCors("Production");
            app.UseHsts();
        }
        else
        {
            app.UseCors("Default");
            app.UseDeveloperExceptionPage();
        }

        app.UseGlobalizationConfig()
           .UseHttpsRedirection()
           .UseMiddleware<ExceptionMiddleware>()
           .UseMiddleware<SecurityMiddleware>(app.Environment)
           .UseStaticFiles()
           .UseRouting()
           .UseAuthentication()
           .UseAuthorization();

        return app;
    }

    public static WebApplication UseEndPointsConfiguration(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}