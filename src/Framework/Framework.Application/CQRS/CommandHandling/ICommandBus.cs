namespace Framework.Application.CQRS.CommandHandling;

public interface ICommandBus
{
    Task DispatchAsync<TCommand>(TCommand command, bool activeTransaction = false, CancellationToken cancellation = default) where TCommand : ICommand;
    ICommandFluent Execute<TCommand>(TCommand command) where TCommand : ICommand;
}
