using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.ModuloFinanceiro.Domain;

namespace DevXpert.Modulo3.ModuloFinanceiro.Data.Repository;

public class PagamentoRepository(PagamentoContext context) : IPagamentoRepository
{
    public IUnitOfWork UnitOfWork => context;

    public Task<bool> RealizarPagamento(Domain.Pagamento pagamento)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {

    }

   

}
