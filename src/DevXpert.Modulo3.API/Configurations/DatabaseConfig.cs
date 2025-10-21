using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.ModuloFinanceiro.Data;
using DevXpert.Modulo3.ModuloAluno.Data;
using DevXpert.Modulo3.ModuloConteudo.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class DatabaseConfig
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;
        var configuration = builder.Configuration;

        if (env.IsDevelopment())
        {
            builder.Services
                   .AddDbContext<IdentityAppContext>(options => options.UseSqlite(configuration.GetConnectionString("IdentitySqliteConnectionLite"),
                                                                                  opt => opt.CommandTimeout(45)
                                                                                            .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            builder.Services
                   .AddDbContext<CursoContext>(options => options.UseSqlite(configuration.GetConnectionString("CursoSqliteConnectionLite"),
                                                                            opt => opt.CommandTimeout(45)
                                                                                      .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            builder.Services
                   .AddDbContext<AlunoContext>(options => options.UseSqlite(configuration.GetConnectionString("AlunoSqliteConnectionLite"),
                                                                            opt => opt.CommandTimeout(45)
                                                                                      .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            builder.Services
                   .AddDbContext<PagamentoContext>(options => options.UseSqlite(configuration.GetConnectionString("PagamentoSqliteConnectionLite"),
                                                                                opt => opt.CommandTimeout(45)
                                                                                          .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            return builder;
        }

        builder.Services
               .AddDbContext<IdentityAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                                                                                 opt => opt.CommandTimeout(45)
                                                                                           .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                           .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

        builder.Services
               .AddDbContext<CursoContext>(options => options.UseSqlServer(configuration.GetConnectionString("CursoConnection"),
                                                                           opt => opt.CommandTimeout(45)
                                                                                     .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                     .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

        builder.Services
               .AddDbContext<AlunoContext>(options => options.UseSqlServer(configuration.GetConnectionString("AlunoConnection"),
                                                                           opt => opt.CommandTimeout(45)
                                                                                     .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                     .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

        builder.Services
               .AddDbContext<PagamentoContext>(options => options.UseSqlServer(configuration.GetConnectionString("PagamentoConnection"),
                                                                               opt => opt.CommandTimeout(45)
                                                                                         .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                         .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
        return builder;
    }

    public static async Task<WebApplication> MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await scope.ServiceProvider.GetService<IdentityAppContext>().Database.MigrateAsync();
        await scope.ServiceProvider.GetService<CursoContext>().Database.MigrateAsync();
        //await scope.ServiceProvider.GetService<AlunoContext>().Database.MigrateAsync();
        //await scope.ServiceProvider.GetService<PagamentoContext>().Database.MigrateAsync();

        return app;
    }
}
