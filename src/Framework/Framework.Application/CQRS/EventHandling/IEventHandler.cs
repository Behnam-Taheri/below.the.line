using Framework.Core.Abstractions;

namespace Framework.Application.CQRS.EventHandling;

public interface IEventHandler
{
}
public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
{
    public Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
