using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

[DisplayName("Món ăn")]
public partial class Dish
{
    [DisplayName("Mã món ăn")]
    public int DishId { get; set; }
    [DisplayName("Tên món ăn")]
    public string? DishName { get; set; }
    [DisplayName("Giá")]
    public decimal Price { get; set; }

    public int DishTypeId { get; set; }
    [DisplayName("Đơn vị tính")]
   
    public int UnitId { get; set; }

    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = new List<DetailInvoice>();

    public virtual DishType DishType { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
