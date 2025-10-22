namespace DevXpert.Modulo3.Core.Application.ViewModels;

public class AuthResultViewModel
{
    public bool Sucesso { get; set; }
    public string Token { get; set; }
    public List<string> Erros { get; set; }

    public AuthResultViewModel(string token)
    {
        Sucesso = true;
        Token = token;
        Erros = [];
    }

    public AuthResultViewModel(List<string> erros)
    {
        Sucesso = false;
        Erros = erros;
    }
}