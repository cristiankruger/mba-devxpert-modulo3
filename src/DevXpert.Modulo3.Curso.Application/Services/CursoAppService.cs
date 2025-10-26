using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;
using DevXpert.Modulo3.ModuloConteudo.Domain;
using DevXpert.Modulo3.ModuloConteudo.Application.Mapper;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;

namespace DevXpert.Modulo3.ModuloConteudo.Application.Services
{
    public class CursoAppService(ICursoRepository cursoRepository,
                                 IMediatrHandler mediatorHandler) : ICursoAppService
    {
        //Curso
        public async Task<CursoViewModel> ObterPorId(Guid id)
        {
            return CursoMappingProfile.MapCursoToCursoViewModel(await cursoRepository.Obter(id));
        }

        public async Task<IEnumerable<CursoViewModel>> ObterTodos()
        {
            return CursoMappingProfile.MapListaCursoToCursoViewModel(await cursoRepository.ObterTodos());
        }

        public async Task AdicionarCurso(CursoViewModel cursoViewModel)
        {
            var curso = CursoMappingProfile.MapCursoViewModelToCurso(cursoViewModel);

            var jaCadastrado = await cursoRepository.Buscar(c => c.Nome == curso.Nome);

            if (jaCadastrado.Any())
            {
                await mediatorHandler.PublicarNotificacao(new DomainNotification("Curso", "Já existe um curso com este nome cadastrado."));
                return;
            }

            await cursoRepository.Adicionar(curso);
            await cursoRepository.UnitOfWork.Commit();
        }      

        //Aula
        public async Task<AulaViewModel> ObterAulaPorId(Guid id)
        {
            return CursoMappingProfile.MapAulaToAulaViewModel(await cursoRepository.ObterAula(id));
        }

        public async Task<IEnumerable<AulaViewModel>> ObterAulas(Guid cursoId)
        {
            return CursoMappingProfile.MapListaAulaToAulaViewModel(await cursoRepository.ObterAulas(cursoId));
        }

        public async Task AdicionarAula(AulaViewModel aulaViewModel)
        {
            var curso = await cursoRepository.Obter(aulaViewModel.CursoId);

            if (curso is null)
            {
                await mediatorHandler.PublicarNotificacao(new DomainNotification("Curso", "Curso não encontrado."));
                return;
            }

            var aula = CursoMappingProfile.MapAulaViewModelToAula(aulaViewModel);

            var jaCadastrado = curso.Aulas.FirstOrDefault(a => a.CursoId == aula.CursoId && a.Conteudo == aula.Conteudo);

            if (jaCadastrado is not null)
            {
                await mediatorHandler.PublicarNotificacao(new DomainNotification("Curso", "Já existe uma aula com este título para este curso."));
                return;
            }

            curso.CadastrarAula(aula);
            await cursoRepository.Adicionar(aula);
            await cursoRepository.UnitOfWork.Commit();
        }

    }
}
