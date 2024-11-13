using Framework.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application.CQRS.EventHandling;

public class EventBus : IEventBus
{
    private readonly IList<object> _subscribers;
    private readonly IServiceProvider _serviceProvider;
    public EventBus(IServiceProvider serviceProvider)
    {
        _subscribers = new List<object>();
        _serviceProvider = serviceProvider;
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (@event != null)
        {
            @event.PublishedOn = DateTime.Now;

            //if (@event is not IIntegrationEvent)
            //    await FireEventHandlers(@event);

            //if (@event is IIntegrationEvent)
            //    await FireIntegrationEvent(@event as IIntegrationEvent);

            FireSubscribers(@event);
        }
    }

    public void Subscribe<TEvent>(Action<TEvent> eventHandler) where TEvent : IEvent
    {
        _subscribers.Add(eventHandler);
    }

    private async Task FireEventHandlers<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var handler = _serviceProvider.GetService<IEventHandler<TEvent>>();

        if (handler != null)
            await handler.HandleAsync(@event, CancellationToken.None);
    }

    private async Task FireIntegrationEvent<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
    {
        //var handler = _serviceProvider.GetService<IEventHandler<TEvent>>();

        //if (handler == null)
        //    throw new InvalidOperationException($"No EventHandler is registered for {typeof(TEvent).Name}");

        //await handler.HandleAsync(@event, CancellationToken.None);
    }

    private void FireSubscribers<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var actions = _subscribers.OfType<Action<TEvent>>().ToList();
        if (actions.Any())
        {
            actions.ForEach(subscriber =>
            {
                subscriber(@event);
                Unsubscribe(subscriber);
            });
        }
    }

    private void Unsubscribe<TEvent>(Action<TEvent> subscriber) where TEvent : IEvent
    {
        _subscribers.Remove(subscriber);
    }
}