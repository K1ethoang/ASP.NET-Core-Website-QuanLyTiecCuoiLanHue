using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class DetailInvoice
{
    public int DetailInvoiceId { get; set; }

    public int? Number { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    public int DishId { get; set; }

    public int InvoiceId { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Invoice Invoice { get; set; } = null!;
}
