using DevXpert.Modulo3.Core.DTO;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.IntegrationEvents;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Services;

public class PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                              IPagamentoRepository pagamentoRepository,
                              IMediatrHandler mediatrHandler) : IPagamentoService
{    
    public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
    {
        var pedido = new Pedido(pagamentoPedido.PedidoId, pagamentoPedido.Total);
        var pagamento = new Pagamento(pagamentoPedido.PedidoId, new(pagamentoPedido.NomeCartao, pagamentoPedido.NumeroCartao, pagamentoPedido.CvvCartao, pagamentoPedido.ExpiracaoCartao));

        var transacao = pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

        if (transacao.StatusPagamento == StatusPagamento.Recusado)
        {
            await mediatrHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await mediatrHandler.PublicarEvento(new PedidoPagamentoRecusadoEvent(pedido.Id, pagamentoPedido.AlunoId, transacao.PagamentoId, transacao.Id, pedido.Valor));

            return transacao;
        }

        pagamento.AdicionarEvento(new PedidoPagamentoAprovadoEvent(pedido.Id, pagamentoPedido.AlunoId, transacao.PagamentoId, transacao.Id, pedido.Valor));

        pagamentoRepository.Adicionar(pagamento);
        pagamentoRepository.AdicionarTransacao(transacao);

        await pagamentoRepository.UnitOfWork.Commit();
        return transacao;
    }
}