using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Đơn vị")]
public partial class Unit
{
    public int UnitId { get; set; }
    [DisplayName("Tên đơn vị")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được trống")]
    public string UnitName { get; set; } = null!;
    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
