using AutoMapper;
using DevXpert.Modulo3.Conteudo.Application.ViewModels;
using DevXpert.Modulo3.Conteudo.Domain;

namespace DevXpert.Modulo3.Conteudo.Application.Mapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Curso, CursoViewModel>()
               .ForMember(dest => dest.Instrutor, opt => opt.MapFrom(src => src.ConteudoProgramatico.Instrutor))
               .ForMember(dest => dest.Ementa, opt => opt.MapFrom(src => src.ConteudoProgramatico.Ementa))
               .ForMember(dest => dest.PublicoAlvo, opt => opt.MapFrom(src => src.ConteudoProgramatico.PublicoAlvo));

        CreateMap<Aula, AulaViewModel>()
            .ForMember(dest=> dest.DuracaoEmSegundos, opt => opt.MapFrom(src => src.Duracao.TotalSeconds));
    }
}
