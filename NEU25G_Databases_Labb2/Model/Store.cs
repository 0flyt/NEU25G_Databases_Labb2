using System;
using System.Collections.Generic;

namespace NEU25G_Databases_Labb2;

public partial class Store
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Address { get; set; }

    public int? ZipCode { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Order> OrderDestinationStores { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderSenderStores { get; set; } = new List<Order>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<StoreBook> StoreBooks { get; set; } = new List<StoreBook>();
}
