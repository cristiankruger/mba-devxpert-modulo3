using DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;
using DevXpert.Modulo3.ModuloConteudo.Domain;

namespace DevXpert.Modulo3.ModuloConteudo.Application.Mapper;

public static class CursoMappingProfile
{
    //CURSO
    public static Curso MapCursoViewModelToCurso(CursoViewModel model)
    {
        if (model is null) return null;
        var curso = new Curso(model.Id, model.Nome, new(model.Instrutor, model.Ementa, model.PublicoAlvo));
        
        foreach (var aula in model.Aulas)
            curso.CadastrarAula(MapAulaViewModelToAula(aula));

        return curso;
    }

    public static IEnumerable<Curso> MapListaCursoViewModelToCurso(IEnumerable<CursoViewModel> listaViewModel)
    {
        if (listaViewModel is null) return null;

        var lista = new List<Curso>();

        foreach (var viewModel in listaViewModel)
        {
            var curso = MapCursoViewModelToCurso(viewModel);
            lista.Add(curso);
        }

        return lista;
    }

    public static CursoViewModel MapCursoToCursoViewModel(Curso entity)
    {
        if (entity is null) return null;

        var viewModel = new CursoViewModel()
        {
            Id = entity.Id,
            Ativo = entity.Ativo,
            Nome = entity.Nome,
            Ementa = entity.ConteudoProgramatico.Ementa,
            Instrutor = entity.ConteudoProgramatico.Instrutor,
            PublicoAlvo = entity.ConteudoProgramatico.PublicoAlvo,
            Aulas = MapListaAulaToAulaViewModel(entity.Aulas)
        };

        return viewModel;
    }

    public static IEnumerable<CursoViewModel> MapListaCursoToCursoViewModel(IEnumerable<Curso> listaEntity)
    {
        if (listaEntity is null) return null;

        var lista = new List<CursoViewModel>();

        foreach (var entity in listaEntity)
        {
            var curso = MapCursoToCursoViewModel(entity);
            lista.Add(curso);
        }

        return lista;
    }

    //AULA
    public static Aula MapAulaViewModelToAula(AulaViewModel model)
    {
        if (model is null) return null;
        
        return new(model.Id, model.CursoId, model.Conteudo, model.Material);
    }

    public static IEnumerable<Aula> MapListaAulaViewModelToAula(IEnumerable<AulaViewModel> listaViewModel)
    {
        if (listaViewModel is null) return null;

        var lista = new List<Aula>();

        foreach (var viewModel in listaViewModel)
        {
            var aula = MapAulaViewModelToAula(viewModel);
            lista.Add(aula);
        }

        return lista;
    }

    public static AulaViewModel MapAulaToAulaViewModel(Aula entity)
    {
        if (entity is null) return null;

        var viewModel = new AulaViewModel()
        {
            Id = entity.Id,
            CursoId = entity.CursoId,
            Curso = entity.Curso.Nome,
            Conteudo = entity.Conteudo,
            Material= entity.Material,
            Ativo = entity.Ativo
        };

        return viewModel;
    }

    public static IEnumerable<AulaViewModel> MapListaAulaToAulaViewModel(IEnumerable<Aula> listaEntity)
    {
        if (listaEntity is null) return null;

        var lista = new List<AulaViewModel>();

        foreach (var entity in listaEntity)
        {
            var aula = MapAulaToAulaViewModel(entity);
            lista.Add(aula);
        }

        return lista;
    }


    //public MappingProfile()
    //{
    //    CreateMap<Curso, CursoViewModel>()
    //           .ForMember(dest => dest.Instrutor, opt => opt.MapFrom(src => src.ConteudoProgramatico.Instrutor))
    //           .ForMember(dest => dest.Ementa, opt => opt.MapFrom(src => src.ConteudoProgramatico.Ementa))
    //           .ForMember(dest => dest.PublicoAlvo, opt => opt.MapFrom(src => src.ConteudoProgramatico.PublicoAlvo));

    //    CreateMap<Aula, AulaViewModel>()
    //        .ForMember(dest => dest.DuracaoEmSegundos, opt => opt.MapFrom(src => src.Duracao.TotalSeconds));

    //    CreateMap<CursoViewModel, Curso>()
    //          .ConstructUsing(c => new Curso(c.Nome, new ConteudoProgramatico(c.Instrutor, c.Ementa, c.PublicoAlvo)));

    //    CreateMap<AulaViewModel, Aula>()
    //        .ConstructUsing(a => new Aula(a.CursoId, a.Titulo, a.Link, TimeSpan.FromSeconds(a.DuracaoEmSegundos)));
    //}
}
