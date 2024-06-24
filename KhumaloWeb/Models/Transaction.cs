using System;
using System.Collections.Generic;

namespace KhumaloWeb.Models;

public partial class Transaction
{
    public int TransactionsId { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
