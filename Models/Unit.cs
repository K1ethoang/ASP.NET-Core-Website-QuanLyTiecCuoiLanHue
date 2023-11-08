using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Đơn vị")]
public partial class Unit
{
    [Required(ErrorMessage = "{0} không được trống")]
    [DisplayName("Mã đơn vị tính")]
    public int UnitId
    {
        get; set;
    }
    [Required(ErrorMessage = "{0} không được trống")]
    [Display(Name = "Tên đơn vị tính", Prompt = "Vd: Tô")]
    public string UnitName { get; set; } = null!;


    [Display(Name = "Mô tả", Prompt = "Vd: 1 đĩa")]
    public string? Description
    {
        get; set;
    }

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
