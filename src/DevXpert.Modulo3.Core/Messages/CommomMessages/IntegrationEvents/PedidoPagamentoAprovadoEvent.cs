namespace DevXpert.Modulo3.Core.Messages.CommomMessages.IntegrationEvents;

public class PedidoPagamentoAprovadoEvent : IntegrationEvent
{
    public Guid PedidoId { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid PagamentoId { get; private set; }
    public Guid TransacaoId { get; private set; }
    public decimal Total { get; private set; }

    public PedidoPagamentoAprovadoEvent(Guid pedidoId, Guid alunoId, Guid pagamentoId, Guid transacaoId, decimal total)
    {
        AggregateId = pedidoId;
        PedidoId = pedidoId;
        AlunoId = alunoId;
        PagamentoId = pagamentoId;
        TransacaoId = transacaoId;
        Total = total;
    }
}
