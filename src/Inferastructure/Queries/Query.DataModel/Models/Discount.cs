using System;
using System.Collections.Generic;

namespace Query.DataModel.Models;

public partial class Discount
{
    public long Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal OriginalPrice { get; set; }

    public int DiscountedPrice { get; set; }

    public string ImagePath { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreateDate { get; set; }
}
