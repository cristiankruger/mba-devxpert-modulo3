using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.ModuloFinanceiro.Data.Repository;

public class PagamentoRepository(PagamentoContext context) : IPagamentoRepository
{
    public IUnitOfWork UnitOfWork => context;

    public void Adicionar(Pagamento pagamento)
    {
        context.Pagamentos.Add(pagamento);
    }

    public void AdicionarTransacao(Transacao transacao)
    {
        context.Transacoes.Add(transacao);
    }

    public void Dispose()
    {

    }
}
