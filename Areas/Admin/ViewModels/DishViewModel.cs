using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    [Keyless]
    public class DishViewModel
    {
        [DisplayName("Mã món ăn")]
        public int DishId
        {
            get; set;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} không được trống")]
        [Display(Name = "Tên món ăn", Prompt = "Vd: Cơm chiên cá mặn")]
        public string? DishName
        {
            get; set;
        }
        [Required(ErrorMessage ="{0} không được trống")]
        [Range(10000, 999999999, ErrorMessage = "{0} trong khoảng [{1} và {2}]")]
        [Display(Name ="Giá", Prompt ="Vd: 250000")]
        public int Price
        {
            get; set;
        }

        [Required]
        [DisplayName("Loại món ăn")]
        public int DishTypeId
        {
            get; set;
        }

        [Required]
        [DisplayName("Đơn vị tính")]
        public int UnitId
        {
            get; set;
        }

        public DishViewModel()
        {
        }

        public DishViewModel(Dish dish) : this()
        {
            DishId = dish.DishId;
            DishName = dish.DishName;
            Price = Convert.ToInt32(dish.Price);
            DishTypeId = dish.DishTypeId;
            UnitId = Convert.ToInt32(dish.UnitId);
        }

        public Dish ToDish(QlDichVuNauTiecLanHueContext context)
        {
            return new Dish()
            {
                DishId = DishId,
                DishName = DishName,
                Price = Price,
                DishTypeId = DishTypeId,
                UnitId = UnitId,
                // Foreign Object
                DishType = context.DishTypes.Find(DishTypeId) ?? new DishType(),
                Unit = context.Units.Find(UnitId) ?? new Unit(),
            };
        }
    }
}
