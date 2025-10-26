using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

public class Transacao : Entity
{
    public Guid PagamentoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataTransacao { get; set; }
    public StatusPagamento StatusPagamento { get; set; }

    public Transacao()
    {

    }

    /*EF Relation*/
    public Pagamento Pagamento { get; set; }
    /*EF Relation*/
}
