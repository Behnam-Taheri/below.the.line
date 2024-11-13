using BTL.Application.Contract.Discounts.Commands;
using BTL.Domain.Discounts;
using BTL.Domain.Discounts.Arguments;
using BTL.Domain.Discounts.Contracts;
using Framework.Application.CQRS.CommandHandling;
using Framework.Application.CQRS.EventHandling;
using Framework.IdGen;

namespace BTL.Application.Discounts.CommandHandlers
{
    public class CreateDiscountCommandHandler : CommandHandler<CreateDiscountCommand>
    {
        private readonly IDiscountRepository _repository;
        private readonly IIdGenerator _idGenerator;
        public CreateDiscountCommandHandler(IEventBus eventBus, IDiscountRepository repository,
            IIdGenerator idGenerator) : base(eventBus)
        {
            _repository = repository;
            _idGenerator = idGenerator;
        }

        public override async Task HandleAsync(CreateDiscountCommand command, CancellationToken cancellationToken)
        {
            var discount = new Discount(MapToArgument(command));
            await _repository.CreateAsync(discount, cancellationToken);
        }

        private static CreateDiscountArg MapToArgument(CreateDiscountCommand command) => new()
        {
            Brand = command.Brand,
            ProductBarcode = command.ProductBarcode,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            ImagePath = command.ImagePath,
            OriginalPrice = command.OriginalPrice,
            DiscountedPrice = command.DiscountedPrice,
            ProductName = command.ProductName,
        };
    }
}
