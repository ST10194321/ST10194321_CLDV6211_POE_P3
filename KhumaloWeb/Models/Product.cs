using System;
using System.Collections.Generic;

namespace KhumaloWeb.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? UserId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Category { get; set; }

    public string Availability { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? User { get; set; }
}
