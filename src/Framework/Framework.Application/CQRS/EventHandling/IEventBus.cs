using Framework.Core.Abstractions;

namespace Framework.Application.CQRS.EventHandling;

public interface IEventBus
{
    void Subscribe<TEvent>(Action<TEvent> eventHandler) where TEvent : IEvent;

    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}
