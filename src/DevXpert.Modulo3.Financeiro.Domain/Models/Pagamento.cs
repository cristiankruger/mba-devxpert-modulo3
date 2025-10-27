using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

public class Pagamento : Entity, IAggregateRoot
{
    public Guid PedidoId { get; private set; }
    public DadosCartao DadosCartao { get; private set; }

    /*EF RELATION*/
    public Transacao Transacao { get; set; }
    /*EF RELATION*/

    public Pagamento()
    {

    }

    public Pagamento(Guid pedidoId, DadosCartao dadosCartao)
    {
        PedidoId = pedidoId;
        DadosCartao = dadosCartao;
    }
}
