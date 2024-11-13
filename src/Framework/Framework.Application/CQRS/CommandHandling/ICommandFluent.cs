using Framework.Core.Abstractions;

namespace Framework.Application.CQRS.CommandHandling;

public interface ICommandFluent
{
    ICommandFluent On<TEvent>(Action<TEvent> action) where TEvent : IEvent;

    Task DispatchAsync(bool activeTransaction = false, CancellationToken cancellation = default);
}
