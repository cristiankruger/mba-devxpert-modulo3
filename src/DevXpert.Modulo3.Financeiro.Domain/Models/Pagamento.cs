using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

public class Pagamento : Entity, IAggregateRoot
{
    public Guid PedidoId { get; set; }
    public DadosCartao DadosCartao { get; set; }
    public Transacao Transacao { get; set; }

    public Pagamento() { }       
}
