using Framework.Application.CQRS.EventHandling;
using Framework.Core.Abstractions;

namespace Framework.Application.CQRS.CommandHandling;

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly IEventBus eventBus;

    protected CommandHandler(IEventBus eventBus)
    {
        this.eventBus = eventBus;
    }

    public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);

    public async Task PublishAggregatedEvents(IAggregateRoot aggregateRoot)
    {
        var events = aggregateRoot.GetAllQueuedEvents();
        // events.Select(async @event => await PublishEvent(@event));

        foreach (var @event in events)
        {
            await PublishEvent(@event);
        }
    }

    public async Task PublishEvent<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (@event is null)
            throw new ArgumentNullException();

        await eventBus.Publish(@event);
    }
}