using System.ComponentModel.DataAnnotations;

namespace DevXpert.Modulo3.Conteudo.Application.ViewModels;

public class AulaViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O Id do Curso é obrigatório.")]
    public Guid CursoId { get; set; }

    [Required(ErrorMessage = "O Link da aula é obrigatório.")]
    [StringLength(maximumLength: 250, MinimumLength = 10, ErrorMessage = "O link da aula deve ter entre 10 e 250 caracteres.")]
    public string Link { get; set; }

    [Required(ErrorMessage = "O Título da aula é obrigatório.")]
    [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "O título da aula deve ter entre 10 e 100 caracteres.")]
    public string Titulo { get; set; }

    [Range(1, 7200, ErrorMessage = "A duração da aula deve ser entre 1 segundo e 2 horas.")]
    public int DuracaoEmSegundos { get; set; }

    public bool Ativo { get; set; } = true;    
}
