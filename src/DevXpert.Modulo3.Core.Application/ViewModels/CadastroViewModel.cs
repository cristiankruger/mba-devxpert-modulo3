using System.ComponentModel.DataAnnotations;

namespace DevXpert.Modulo3.Core.Application.ViewModels;

public class CadastroViewModel
{
    [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo E-mail deve ser um endereço de e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$%*¨'+:&/()=!?@{}\-;<>\]\[_.,^~])[A-Za-z\d#$%*¨'+:&/()=!?@{}\-;<>\]\[_.,^~]{6,}$", ErrorMessage = "O campo {0} precisa ter ao menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial.")]
    public string Senha { get; set; }

    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string SenhaConfirmacao { get; set; }

    public bool EhAluno { get; set; } = true;
}
