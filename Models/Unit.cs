using System;
using System.Collections.Generic;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
