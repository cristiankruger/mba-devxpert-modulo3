namespace DevXpert.Modulo3.Core.Domain;

public class Admin : Usuario
{
    public Admin()
    {

    }

    public Admin(Guid id, string email, string nome)
    {
        Id = id;
        Email = email;
        Nome = nome;
        Ativar();
    }

}