using Framework.Application.CQRS.EventHandling;
using Framework.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application.CQRS.CommandHandling;

public class CommandBus : ICommandBus, ICommandFluent
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventBus _eventBus;
    private readonly IUnitOfWork _unitOfWork;
    private readonly dynamic _commandToDispatch;

    public CommandBus(IServiceProvider serviceProvider, IEventBus eventBus)
    {
        _serviceProvider = serviceProvider;
        _eventBus = eventBus;
    }

    private CommandBus(IServiceProvider serviceProvider, IEventBus eventBus, dynamic commandToDispatch) : this(serviceProvider, eventBus)
    {
        _commandToDispatch = commandToDispatch;
    }

    public async Task DispatchAsync<TCommand>(TCommand command, bool activeTransaction = false, CancellationToken cancellation = default) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

        if (handler == null)
            throw new InvalidOperationException($"No CommandHandler is registered for {typeof(TCommand).Name}");

        if (activeTransaction)
        {
            var handlerWithCommit = new UnitOfWorkDecorator<TCommand>(handler, _unitOfWork);
            await handlerWithCommit.HandleAsync(command, cancellation);
        }
        else
            await handler.HandleAsync(command, cancellation);
    }

    public ICommandFluent Execute<TCommand>(TCommand command) where TCommand : ICommand
    {
        return new CommandBus(_serviceProvider, _eventBus, command);
    }

    public ICommandFluent On<TEvent>(Action<TEvent> action) where TEvent : IEvent
    {
        _eventBus.Subscribe(action);
        return this;
    }

    public async Task DispatchAsync(bool activeTransaction = false, CancellationToken cancellation = default)
    {
        await DispatchAsync(_commandToDispatch, activeTransaction, cancellation);
    }

}