using System.ComponentModel.DataAnnotations;

namespace DevXpert.Modulo3.ModuloConteudo.Application.ViewModels;

public class CursoViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome do curso é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do curso deve ter no máximo 150 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O Instrutor do curso é obrigatório.")]
    [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "O nome do instrutor deve ter entre 5 e 100 caracteres.")]
    public string Instrutor { get; set; }

    [Required(ErrorMessage = "O público Alvo do curso é obrigatório.")]
    [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "O público Alvo do curso é obrigatório.")]
    public string PublicoAlvo { get; set; }

    [Required(ErrorMessage = "A ementa do curso é obrigatória.")]
    [StringLength(maximumLength: 1000, MinimumLength = 20, ErrorMessage = "A ementa do curso é obrigatória.")]
    public string Ementa { get; set; }

    public bool PermitirMatricula { get; set; }
    public bool Ativo { get; set; }
    public TimeSpan CargaHoraria { get; set; }
    public IEnumerable<AulaViewModel> Aulas { get; set; }

    public CursoViewModel()
    {
        Id = Guid.NewGuid();
        PermitirMatricula = false;
        Ativo = true;
        CargaHoraria = TimeSpan.FromSeconds(0);
        Aulas = [];
    }

}
