using DevXpert.Modulo3.Core.Data;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void Adicionar(Pagamento pagamento);
    void AdicionarTransacao(Transacao transacao);
}