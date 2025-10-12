using AutoMapper;
using DevXpert.Modulo3.Conteudo.Application.ViewModels;
using DevXpert.Modulo3.Conteudo.Domain;

namespace DevXpert.Modulo3.Conteudo.Application.Mapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<CursoViewModel, Curso>()
               .ConstructUsing(c => new Curso(c.Nome, new ConteudoProgramatico(c.Instrutor, c.Ementa,  c.PublicoAlvo)));

        CreateMap<AulaViewModel,Aula>()
            .ConstructUsing(a=> new Aula(a.CursoId, a.Link, TimeSpan.FromSeconds(a.DuracaoEmSegundos)));
    }
}
