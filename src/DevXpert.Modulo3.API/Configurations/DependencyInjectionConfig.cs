using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Core.Application.Services;
using DevXpert.Modulo3.Core.Data;
using CoreRepository = DevXpert.Modulo3.Core.Data.Repository;
using CoreDomain = DevXpert.Modulo3.Core.Domain;
using DevXpert.Modulo3.ModuloAluno.Data;
using DevXpert.Modulo3.ModuloAluno.Data.Repository;
using DevXpert.Modulo3.ModuloAluno.Domain;
using DevXpert.Modulo3.ModuloConteudo.Application.Services;
using DevXpert.Modulo3.ModuloConteudo.Data;
using DevXpert.Modulo3.ModuloConteudo.Data.Repository;
using DevXpert.Modulo3.ModuloConteudo.Domain;
using DevXpert.Modulo3.ModuloFinanceiro.Data;
using DevXpert.Modulo3.ModuloFinanceiro.Data.Repository;
using DevXpert.Modulo3.ModuloFinanceiro.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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

        //IDENTITY
        builder.Services.AddScoped<IAuthAppService, AuthAppService>();
        builder.Services.AddScoped<CoreDomain.IAdminRepository, CoreRepository.AdminRepository>();
        builder.Services.AddScoped<CoreDomain.IAlunoRepository, CoreRepository.AlunoRepository>();

        //CURSO
        builder.Services.AddScoped<ICursoAppService, CursoAppService>();
        builder.Services.AddScoped<ICursoRepository, CursoRepository>();

        //ALUNO
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

        //PAGAMENTO
        builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();

        return builder;
    }
}
