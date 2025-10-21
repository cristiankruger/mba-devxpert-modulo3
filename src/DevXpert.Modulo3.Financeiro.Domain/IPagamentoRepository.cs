using DevXpert.Modulo3.Core.Data;

namespace DevXpert.Modulo3.ModuloFinanceiro.Domain;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    Task<bool> RealizarPagamento(Pagamento pagamento);
}
