using System;
using System.Collections.Generic;

namespace NEU25G_Databases_Labb2;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Book Isbn13Navigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
