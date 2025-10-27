using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

public class Transacao : Entity
{
    public Guid PagamentoId { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataTransacao { get; private set; }
    public StatusPagamento StatusPagamento { get; private set; }

    /*EF Relation*/
    public Pagamento Pagamento { get; set; }
    /*EF Relation*/

    public Transacao()
    {
        StatusPagamento = StatusPagamento.AguardandoPagamento;
        DataTransacao = DateTime.Now;
    }

    public Transacao(Guid pagamentoId, decimal valor)
    {
        Valor = valor;
        PagamentoId = pagamentoId;
        StatusPagamento = StatusPagamento.AguardandoPagamento;
        DataTransacao = DateTime.Now;
    }

    public void TransacaoAprovada()
    {
        StatusPagamento = StatusPagamento.Aprovado;
    }

    public void TransacaoRecusada()
    {
        StatusPagamento = StatusPagamento.Recusado;
    }
}
