using BTL.Domain.Discounts.Arguments;
using Framework.Domain.Abstractions;

namespace BTL.Domain.Discounts
{
    public class Discount : AggregateRoot<long>
    {
        private Discount() { }
        public Discount(CreateDiscountArg arg)
        {
            SetProperties(arg);
        }

        public string Brand { get; private set; }
        public string Barcode { get; private set; }
        public string ProductName { get; private set; }
        public decimal OriginalPrice { get; private set; }
        public int DiscountedPrice { get; private set; }
        public string ImagePath { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }


        public void SetProperties(CreateDiscountArg arg)
        {
            Id = arg.Id;
            Brand = arg.Brand;
            Barcode = arg.ProductBarcode;
            DiscountedPrice = arg.DiscountedPrice;
            StartDate = arg.StartDate;
            EndDate = arg.EndDate;
            ImagePath = arg.ImagePath;
            OriginalPrice = arg.OriginalPrice;
            ProductName = arg.ProductName;
            CreateDate = DateTime.Now;
        }
    }
}
