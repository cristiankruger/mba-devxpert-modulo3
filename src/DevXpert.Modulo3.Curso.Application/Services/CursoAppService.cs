using DevXpert.Modulo3.Conteudo.Application.Mapper;
using DevXpert.Modulo3.Conteudo.Application.ViewModels;
using DevXpert.Modulo3.Conteudo.Domain;
using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.Conteudo.Application.Services
{
    public class CursoAppService(ICursoRepository cursoRepository) : ICursoAppService
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
                throw new DomainException("Já existe um curso com este nome cadastrado.");

            await cursoRepository.Adicionar(curso);
            await cursoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarCurso(CursoViewModel cursoViewModel)
        {
            var curso = CursoMappingProfile.MapCursoViewModelToCurso(cursoViewModel);

            var jaCadastrado = await cursoRepository.Buscar(c => c.Nome == curso.Nome && c.Id != curso.Id);

            if (jaCadastrado.Any())
                throw new DomainException("Já existe um curso com este nome cadastrado.");

            cursoRepository.Atualizar(curso);
            await cursoRepository.UnitOfWork.Commit();
        }

        public async Task PermitirInscricaoCurso(Guid id)
        {
            var curso = CursoMappingProfile.MapCursoViewModelToCurso(await ObterPorId(id)) ??
                throw new DomainException("Curso não encontrado.");

            if (curso.Aulas.Count == 0)
                throw new DomainException("Não é possível permitir inscrição em um curso sem aulas.");

            curso.PermitirInscricao();
            cursoRepository.Atualizar(curso);
            await cursoRepository.UnitOfWork.Commit();
        }

        public async Task ProibirInscricaoCurso(Guid id)
        {
            var curso = CursoMappingProfile.MapCursoViewModelToCurso(await ObterPorId(id)) ??
                throw new DomainException("Curso não encontrado.");

            curso.ProibirInscricao();
            cursoRepository.Atualizar(curso);
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
            var curso = await cursoRepository.Obter(aulaViewModel.CursoId) ??
                throw new DomainException("Curso não encontrado.");

            var aula = CursoMappingProfile.MapAulaViewModelToAula(aulaViewModel);

            var jaCadastrado = curso.Aulas.FirstOrDefault(a => a.CursoId == aula.CursoId && a.Titulo == aula.Titulo);

            if (jaCadastrado is not null)
                throw new DomainException("Já existe uma aula com este título para este curso.");

            curso.CadastrarAula(aula);
            await cursoRepository.Adicionar(aula);
            await cursoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarAula(AulaViewModel aulaViewModel)
        {
            var aula = CursoMappingProfile.MapAulaViewModelToAula(await ObterAulaPorId(aulaViewModel.Id)) ??
                throw new DomainException("Aula não encontrada.");

            var curso = await cursoRepository.Obter(aulaViewModel.CursoId) ??
                throw new DomainException("Curso não encontrado.");

            var jaCadastrado = curso.Aulas.Any(a => a.Titulo == aula.Titulo && a.Id != aula.Id);

            if (jaCadastrado)
                throw new DomainException("Já existe uma aula com este título para este curso.");

            cursoRepository.Atualizar(aula);
            await cursoRepository.UnitOfWork.Commit();
        }

    }
}
