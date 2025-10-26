namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

public class Pedido
{
    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public Guid MatriculaId { get; set; }
    public decimal Valor { get; set; }
}
