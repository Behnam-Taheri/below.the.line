
using Framework.Application.CQRS.CommandHandling;

namespace BTL.Application.Contract.Discounts.Commands
{
    public record CreateDiscountCommand() : ICommand
    {
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string ProductBarcode { get; set; }
        public decimal OriginalPrice { get; set; }
        public int DiscountedPrice { get; set; }
        public string ImagePath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
