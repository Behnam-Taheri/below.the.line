namespace Query.Retrieval.Discounts.Response
{
    public record DiscountListQueryResponse
    {
        public long Id { get; set; }
        public string Brand { get; set; } = null!;
        public string ProductBarcode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal OriginalPrice { get; set; }
        public int DiscountedPrice { get; set; }
        public string ImagePath { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }

        private int percentageDiscount;

        public int PercentageDiscount
        {
            get { return CalcPercentage(); }
            set { percentageDiscount = value; }
        }

        private int CalcPercentage()
        {
            decimal difference = Math.Abs(OriginalPrice - DiscountedPrice);
            decimal percentageDifference = (difference * 100)/ OriginalPrice;
            return (int)percentageDifference;
            
        }
    }
}
