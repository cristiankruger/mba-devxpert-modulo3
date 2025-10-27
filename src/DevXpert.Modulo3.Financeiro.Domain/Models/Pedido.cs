namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

public class Pedido
{
    public Guid Id { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid MatriculaId { get; private set; }
    public decimal Valor { get; private set; }

    public Pedido()
    {
        
    }

    public Pedido(Guid id, decimal valor)
    {
        Id = id;
        Valor = valor;
    }
}
