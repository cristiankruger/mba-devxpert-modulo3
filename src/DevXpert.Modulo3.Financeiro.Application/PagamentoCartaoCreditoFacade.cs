using DevXpert.Modulo3.ModuloFinanceiro.Business.Interfaces;
using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;

namespace DevXpert.Modulo3.ModuloFinanceiro.AntiCorruption;

public class PagamentoCartaoCreditoFacade(IPayPalGateway payPalGateway,
                                          IConfigurationManager configManager) : IPagamentoCartaoCreditoFacade
{
    public Transacao RealizarPagamento(Pedido pedido, Pagamento pagamento)
    {
        var apiKey = configManager.GetValue("apiKey");
        var encriptionKey = configManager.GetValue("encriptionKey");

        var serviceKey =  payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
        var cardHashKey = payPalGateway.GetCardHashKey(serviceKey, pagamento.DadosCartao.NumeroCartao);

        var pagamentoResult = payPalGateway.CommitTransaction(cardHashKey, pedido.Id.ToString(), pagamento.Transacao.Valor);

        // TODO: O gateway de pagamentos que deve retornar o objeto transação
        var transacao = new Transacao
        {
            DataTransacao = DateTime.Now,
            StatusPagamento = StatusPagamento.AguardandoPagamento,
            Valor = pedido.Valor,
            PagamentoId = pagamento.Id
        };

        if (pagamentoResult)
        {
            transacao.StatusPagamento = StatusPagamento.Aprovado;
            return transacao;
        }

        transacao.StatusPagamento = StatusPagamento.Recusado;
        return transacao;
    }
}
