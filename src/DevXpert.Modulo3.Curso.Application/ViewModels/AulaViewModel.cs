using DevXpert.Modulo3.API.Configurations.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;

public class AulaViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O Id do Curso é obrigatório.")]
    [GuidNotEmpty("CursoId")]
    public Guid CursoId { get; set; }
    
    [Required(ErrorMessage = "O Conteúdo da aula é obrigatório.")]
    [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "O Conteúdo da aula deve ter entre 5 e 100 caracteres.")]
    public string Conteudo { get; set; }

    public string Material { get; set; }

    public bool Ativo { get; set; }
    public string Curso { get; set; }

    public AulaViewModel()
    {
        Id = Guid.NewGuid();
        Ativo = true;
    }
}
