using MediatR;

namespace DevXpert.Modulo3.Core.Messages.CommomMessages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
        _notifications = [];
    }

    public Task Handle(DomainNotification message, CancellationToken cancellationToken)
    {
        _notifications.Add(message);
        return Task.CompletedTask;
    }

    public virtual List<DomainNotification> ObterNotificacoes()
    {
        return _notifications;
    }

    public virtual bool TemNotificacao()
    {
        return ObterNotificacoes().Count != 0;
    }

    public void Dispose()
    {
        _notifications = [];
    }
}