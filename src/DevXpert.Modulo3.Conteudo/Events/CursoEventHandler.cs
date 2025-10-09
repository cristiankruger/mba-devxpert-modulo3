using MediatR;

namespace DevXpert.Modulo3.Conteudo.Domain.Events;

public class CursoEventHandler(ICursoRepository repository) : INotificationHandler<LiberarInscricaoCursoEvent>
{
    public async Task Handle(LiberarInscricaoCursoEvent mensagem, CancellationToken cancellationToken)
    {
        var curso = await repository.Obter(mensagem.AggregateId);

        //TODO: CONFERIR SE CURSO ESTÁ LIBERADO PARA INSCRICAO ()
    }
}
