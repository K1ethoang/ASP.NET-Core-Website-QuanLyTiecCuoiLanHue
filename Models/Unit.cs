using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

public partial class Unit
{
    public int UnitId { get; set; }
    [DisplayName("Tên đơn vị")]
    public string UnitName { get; set; } = null!;
    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
