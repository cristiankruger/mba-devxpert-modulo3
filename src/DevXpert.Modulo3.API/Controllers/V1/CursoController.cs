using Asp.Versioning;
using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using DevXpert.Modulo3.ModuloConteudo.Application.Services;
using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevXpert.Modulo3.API.Controllers.V1;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class CursoController(ICursoAppService cursoAppService,
                             IAppIdentityUser user,
                             IMediatrHandler mediatrHandler,
                             INotificationHandler<DomainNotification> notifications)
    : MainController(user, mediatrHandler, notifications)
{
    [HttpGet]
    [Authorize(Roles = "Admin,Aluno")]
    public async Task<IActionResult> ObterTodos()
    {
        var cursos = await cursoAppService.ObterTodos();

        return CustomResponse(cursos);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin,Aluno")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var curso = await cursoAppService.ObterPorId(id);

        if (curso is null)
            return NotFound();

        return CustomResponse(curso);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdicionarCurso(CursoViewModel cursoViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await cursoAppService.AdicionarCurso(cursoViewModel);

        return CustomResponse(cursoViewModel);
    }

    [HttpGet("aulas/{cursoId:guid}")]
    public async Task<IActionResult> ObterAulas(Guid cursoId)
    {
        var aulas = await cursoAppService.ObterAulas(cursoId);

        return CustomResponse(aulas);
    }

    [HttpGet("aulas/id/{id:guid}")]
    public async Task<IActionResult> ObterAulaPorId(Guid id)
    {
        var aula = await cursoAppService.ObterAulaPorId(id);

        if (aula is null)
            return NotFound();

        return CustomResponse(aula);
    }

    [HttpPost("aula")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdicionarAula(AulaViewModel aulaViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await cursoAppService.AdicionarAula(aulaViewModel);

        return CustomResponse(aulaViewModel);
    }
}
