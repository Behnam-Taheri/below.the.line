namespace Framework.Application.CQRS.CommandHandling;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}
