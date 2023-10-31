using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
	[Keyless]
	public class DishViewModel
	{
		[Required(AllowEmptyStrings =false, ErrorMessage = "{0} không được trống")]
		[DisplayName("Tên món ăn")]
		public string? DishName { get; set; }
		[Required]
		[DisplayName("Giá")]
		[Range(1000,500000)]
		public int Price { get; set; }

		[Required]
		[DisplayName("Loại món ăn")]
		public int DishTypeId { get; set; }

		[Required]
		[DisplayName("Đơn vị tính")]
		public int UnitId { get; set; }

		DishViewModel(){ }
		public DishViewModel(Dish dish) : this()
		{
			DishName = dish.DishName;
			Price = Convert.ToInt32(dish.Price);
			DishTypeId = dish.DishTypeId;
			UnitId = Convert.ToInt32(dish.UnitId);		
		}

		public Dish ToDish(QlDichVuNauTiecLanHueContext context)
		{
			return new Dish()
			{
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
