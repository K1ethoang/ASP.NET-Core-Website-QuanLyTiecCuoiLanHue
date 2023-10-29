using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    public class DishViewModel
    {
        [DisplayName("Tên món ăn")]
        public string? DishName { get; set; }
        [DisplayName("Giá")]
        public decimal Price { get; set; }

        public int DishTypeId { get; set; }
        [DisplayName("Đơn vị tính")]
        public int UnitId { get; set; }

        public Dish ToDish(QlDichVuNauTiecLanHueContext context)
        {
            return new Dish()
            {
                DishName = DishName,
                Price = Price,
                DishTypeId = DishTypeId,
                UnitId = UnitId,
            };
        }
    }
}
