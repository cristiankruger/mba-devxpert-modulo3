using DevXpert.Modulo3.Core.DTO;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;

public interface IPagamentoService
{
    Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
}
