using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Dish
{
    public int DishId { get; set; }

    public string? DishName { get; set; }

    public decimal Price { get; set; }

    public int DishTypeId { get; set; }

    public int UnitId { get; set; }

    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();

    public virtual DishType DishType { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
