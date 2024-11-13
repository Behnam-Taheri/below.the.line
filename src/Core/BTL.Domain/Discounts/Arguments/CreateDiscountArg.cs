namespace BTL.Domain.Discounts.Arguments
{
    public record CreateDiscountArg
    {
        public long Id { get; init; }
        public string Brand { get; init; } = null!;
        public string ProductBarcode { get; init; } = null!;
        public string ProductName { get; init; } = null!;
        public decimal OriginalPrice { get; init; }
        public int DiscountedPrice { get; init; }
        public string ImagePath { get; init; } = null!;
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
