using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Core.Application.Services;
using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using DevXpert.Modulo3.ModuloAluno.Data;
using DevXpert.Modulo3.ModuloAluno.Data.Repository;
using DevXpert.Modulo3.ModuloAluno.Domain;
using DevXpert.Modulo3.ModuloConteudo.Application.Services;
using DevXpert.Modulo3.ModuloConteudo.Data;
using DevXpert.Modulo3.ModuloConteudo.Data.Repository;
using DevXpert.Modulo3.ModuloConteudo.Domain;
using DevXpert.Modulo3.ModuloFinanceiro.AntiCorruption;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Services;
using DevXpert.Modulo3.ModuloFinanceiro.Data;
using DevXpert.Modulo3.ModuloFinanceiro.Data.Repository;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using CoreDomain = DevXpert.Modulo3.Core.Domain;
using CoreRepository = DevXpert.Modulo3.Core.Data.Repository;
using Pagamento = DevXpert.Modulo3.ModuloFinanceiro.AntiCorruption;

namespace DevXpert.Modulo3.API.Configurations;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder ResolveDependecies(this WebApplicationBuilder builder)
    {
        //CORE|COMMON   
        builder.Services.AddScoped<IMediatrHandler, MediatrHandler>();
        builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();

        //IDENTITY
        builder.Services.AddScoped<IdentityAppContext>();
        builder.Services.AddScoped<IAuthAppService, AuthAppService>();
        builder.Services.AddScoped<CoreDomain.IAdminRepository, CoreRepository.AdminRepository>();
        builder.Services.AddScoped<CoreDomain.IAlunoRepository, CoreRepository.AlunoRepository>();

        //CURSO
        builder.Services.AddScoped<CursoContext>();
        builder.Services.AddScoped<ICursoAppService, CursoAppService>();
        builder.Services.AddScoped<ICursoRepository, CursoRepository>();

        //ALUNO
        builder.Services.AddScoped<AlunoContext>();
        builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

        //PAGAMENTO
        builder.Services.AddScoped<PagamentoContext>();
        builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        builder.Services.AddScoped<IPagamentoService, PagamentoService>();
        builder.Services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
        builder.Services.AddScoped<IPayPalGateway, PayPalGateway>();
        builder.Services.AddScoped<Pagamento.IConfigurationManager, Pagamento.ConfigurationManager>();

        return builder;
    }
}
