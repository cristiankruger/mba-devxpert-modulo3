using Asp.Versioning;
using DevXpert.Modulo3.API.Configurations.App;
using DevXpert.Modulo3.Conteudo.Application.Services;
using DevXpert.Modulo3.Conteudo.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevXpert.Modulo3.API.Controllers.V1;

//[Authorize(Roles = "Admin")]
[ApiVersion("1.0")]
[Route("api/v{version:apiversion}/[controller]")]
public class CursoController(IAppIdentityUser user,
                             ICursoAppService cursoAppService) : MainController(user)
{
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var cursos = await cursoAppService.ObterTodos();

        return CustomResponse(HttpStatusCode.OK, cursos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var curso = await cursoAppService.ObterPorId(id);

        if (curso is null)
            return CustomResponse(HttpStatusCode.NotFound);

        return CustomResponse(HttpStatusCode.OK, curso);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarCurso(CursoViewModel cursoViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await cursoAppService.AdicionarCurso(cursoViewModel);

        return CustomResponse(HttpStatusCode.Created, cursoViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarCurso(Guid id, CursoViewModel cursoViewModel)
    {
        if (id != cursoViewModel.Id)
        {
            NotificarErro("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(HttpStatusCode.BadRequest);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        await cursoAppService.AtualizarCurso(cursoViewModel);
        
        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpPost("permitir-inscricao/{id:guid}")]
    public async Task<IActionResult> PermitirInscricaoCurso(Guid id)
    {
        await cursoAppService.PermitirInscricaoCurso(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpPost("proibir-inscricao/{id:guid}")]
    public async Task<IActionResult> ProibirInscricaoCurso(Guid id)
    {
        await cursoAppService.ProibirInscricaoCurso(id);
        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpGet("aulas/{cursoId:guid}")]
    public async Task<IActionResult> ObterAulas(Guid cursoId)
    {
        var aulas = await cursoAppService.ObterAulas(cursoId);

        return CustomResponse(HttpStatusCode.OK, aulas);
    }

    [HttpGet("aulas/id/{id:guid}")]
    public async Task<IActionResult> ObterAulaPorId(Guid id)
    {
        var aula = await cursoAppService.ObterAulaPorId(id);

        if (aula is null)
            return CustomResponse(HttpStatusCode.NotFound);

        return CustomResponse(HttpStatusCode.OK, aula);
    }

    [HttpPost("aula")]
    public async Task<IActionResult> AdicionarAula(AulaViewModel aulaViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await cursoAppService.AdicionarAula(aulaViewModel);

        return CustomResponse(HttpStatusCode.Created, aulaViewModel);
    }

    [HttpPut("aula/{id:guid}")]
    public async Task<IActionResult> AtualizarAula(Guid id, AulaViewModel aulaViewModel)
    {
        if (id != aulaViewModel.Id)
        {
            NotificarErro("O id informado não é o mesmo que foi passado na query");
            return CustomResponse(HttpStatusCode.BadRequest);
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await cursoAppService.AdicionarAula(aulaViewModel);

        return CustomResponse(HttpStatusCode.NoContent);
    }
}
