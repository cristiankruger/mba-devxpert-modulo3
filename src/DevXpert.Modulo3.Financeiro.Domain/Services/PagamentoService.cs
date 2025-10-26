using DevXpert.Modulo3.Core.DTO;
using DevXpert.Modulo3.Core.Mediator;
using DevXpert.Modulo3.Core.Messages.CommomMessages.IntegrationEvents;
using DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

namespace DevXpert.Modulo3.ModuloFinanceiro.Business.Services;

public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IMediatrHandler _mediatorHandler;

    public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade,
                            IPagamentoRepository pagamentoRepository,
                            IMediatrHandler mediatorHandler)
    {
        _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
        _pagamentoRepository = pagamentoRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
    {
        var pedido = new Pedido
        {
            Id = pagamentoPedido.PedidoId,
            Valor = pagamentoPedido.Total
        };

        var pagamento = new Pagamento
        {
            DadosCartao = new DadosCartao(pagamentoPedido.NomeCartao, pagamentoPedido.NumeroCartao, pagamentoPedido.CvvCartao, pagamentoPedido.ExpiracaoCartao),
            PedidoId = pagamentoPedido.PedidoId,            
        };

        var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

        if (transacao.StatusPagamento == StatusPagamento.Recusado)
        {
            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediatorHandler.PublicarEvento(new PedidoPagamentoRecusadoEvent(pedido.Id, pagamentoPedido.AlunoId, transacao.PagamentoId, transacao.Id, pedido.Valor));

            return transacao;
        }

        pagamento.AdicionarEvento(new PedidoPagamentoAprovadoEvent(pedido.Id, pagamentoPedido.AlunoId, transacao.PagamentoId, transacao.Id, pedido.Valor));

        _pagamentoRepository.Adicionar(pagamento);
        _pagamentoRepository.AdicionarTransacao(transacao);

        await _pagamentoRepository.UnitOfWork.Commit();
        return transacao;
    }
}