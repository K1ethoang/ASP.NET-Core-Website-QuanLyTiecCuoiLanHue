using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Loại món ăn")]
public partial class DishType
{
    public int DishTypeId { get; set; }

    [DisplayName("Tên loại món ăn")]
    public string TypeName { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
