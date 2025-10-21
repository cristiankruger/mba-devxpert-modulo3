using System.ComponentModel.DataAnnotations;

namespace DevXpert.Modulo3.Core.Application.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo E-mail deve ser um endereço de e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string Senha{ get; set; }
}
