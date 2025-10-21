namespace DevXpert.Modulo3.Core.Domain;

public class Aluno : Usuario
{
    
    public Aluno()
    {

    }

    public Aluno(Guid id, string email, string nome)
    {
        Id = id;
        Email = email;
        Nome = nome;
        Ativar();
    }

}