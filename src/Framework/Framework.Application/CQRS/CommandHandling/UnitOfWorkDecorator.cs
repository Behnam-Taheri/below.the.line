using Framework.Core.Abstractions;

namespace Framework.Application.CQRS.CommandHandling
{
    internal class UnitOfWorkDecorator<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> handler;
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkDecorator(ICommandHandler<TCommand> handler, IUnitOfWork unitOfWork)
        {
            this.handler = handler;
            this.unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await handler.HandleAsync(command, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
