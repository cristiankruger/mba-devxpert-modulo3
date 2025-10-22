using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;

namespace DevXpert.Modulo3.ModuloConteudo.Application.Services
{
    public interface ICursoAppService
    {
        Task<CursoViewModel> ObterPorId(Guid id);
        Task<IEnumerable<CursoViewModel>> ObterTodos();
        Task AdicionarCurso(CursoViewModel cursoViewModel);
        Task PermitirInscricaoCurso(Guid id);

        Task<IEnumerable<AulaViewModel>> ObterAulas(Guid cursoId);
        Task<AulaViewModel> ObterAulaPorId(Guid id);
        Task AdicionarAula(AulaViewModel aulaViewModel);
    }
}
