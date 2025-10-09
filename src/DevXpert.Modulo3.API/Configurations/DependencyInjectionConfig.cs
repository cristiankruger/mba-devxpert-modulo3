using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using DevXpert.Modulo3.API.Data;
using DevXpert.Modulo3.Conteudo.Data;
using DevXpert.Modulo3.Aluno.Data;
using DevXpert.Modulo3.Financeiro.Data;
using DevXpert.Modulo3.Aluno.Domain;
using DevXpert.Modulo3.Aluno.Data.Repository;
using DevXpert.Modulo3.Conteudo.Data.Repository;
using DevXpert.Modulo3.Conteudo.Domain;
using DevXpert.Modulo3.Financeiro.Domain;
using DevXpert.Modulo3.Financeiro.Data.Repository;
using DevXpert.Modulo3.API.Configurations.App;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder ResolveDependecies(this WebApplicationBuilder builder)
    {       
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IdentityDbContext, IdentityAppContext>();
        builder.Services.AddScoped<CursoContext>();
        builder.Services.AddScoped<AlunoContext>();
        builder.Services.AddScoped<PagamentoContext>();
        builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();

        //CURSO
        builder.Services.AddScoped<ICursoRepository, CursoRepository>();

        //ALUNO
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

        //PAGAMENTO
        builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();

        return builder;
    }
}
