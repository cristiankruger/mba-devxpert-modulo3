using DevXpert.Modulo3.Core.DomainObjects;

namespace DevXpert.Modulo3.ModuloFinanceiro.Domain;

public class Pagamento : Entity, IAggregateRoot
{
    public DadosCartao DadosCartao { get; set; }
    public StatusPagamento StatusPagamento { get; set; }
    
    protected Pagamento() { }
    
    public void Validar()
    {
        throw new NotImplementedException();
    }
}
