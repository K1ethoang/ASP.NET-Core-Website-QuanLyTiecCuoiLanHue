using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels.Menu;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
	[BindProperties]
	public class Menu
	{
		//[BindProperty(Name = "id")]
		public int PartyId { get; set; }
		public List<MenuItem> Items { get; set; } = new List<MenuItem>();
		public List<MenuItem> AvailableItems { get; set; }
		public int MinQty;

		// CONSTRUCTORS
		public Menu()
		{

		}
		public static List<MenuItem> GetAvailableItems(QlDichVuNauTiecLanHueContext context)
		{


			return context.Dishes
				//.Include(d => d.Unit.UnitName)
				//.Include(d => d.DishType.TypeName)
				.Select(d => new MenuItem()
				{
					DishId = d.DishId,
					DishName = d.DishName!,
					UnitName = d.Unit.UnitName,
					DishType = d.DishType.TypeName,
					Qty = 0
				})
				.ToList();
		}

		private void initMinQty(QlDichVuNauTiecLanHueContext context)
		{
			MinQty = context.Parties.Where(p => p.PartyId == PartyId).First().Quantity ?? 0;
		}
		public Menu(int PartyId, QlDichVuNauTiecLanHueContext context)
		{
			this.PartyId = PartyId;
			MinQty = context.Parties.Where(p => p.PartyId == PartyId).First().Quantity ?? 0;
			AvailableItems = GetAvailableItems(context);
		}
		public Menu(Party party, QlDichVuNauTiecLanHueContext context)
		{
			PartyId = party.PartyId;
			MinQty = context.Parties.Where(p => p.PartyId == PartyId).First().Quantity ?? 0;
			AvailableItems = GetAvailableItems(context);

			if (party.HasMenu) { }
		}

		bool DishExists(int id)
		{
			return Items.Any(i => i.DishId == id);
		}

	}
	[Keyless]
	public class MenuItem
	{
		//
		// Type nesting
		// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/nested-types
		//
		//Menu? FromMenu;
		public int DishId { get; set; }
		public int Price {  get; set; }


		//[DisplayName("Tên món ăn")]
		public string? DishName {  get; set; }
		public string? UnitName {  get; set; }
		public string? DishType {  get; set; }
		//public bool Selected {  get; set; }
		[DisplayName("Số lượng")]
		public int Qty { get; set; }

		//public MenuItem() { }
		//public MenuItem(DishViewModel vm) { }
		//public MenuItem(Dish dish, int qty = 1)
		//{
		//	_DishId = dish.DishId;
		//	Qty = qty;
		//}
		//public MenuItem(int dishId, int qty = 1)
		//{
		//	_DishId = dishId;
		//	Qty = qty;
		//}
		public static MenuItem ToMenuItem(Dish dish, string UnitName)
		{
			return new MenuItem()
			{
				DishId = dish.DishId,
				DishName = dish.DishName,
				UnitName = UnitName,
				Qty = 0
			};
		}

	}
	public class MiniMenuItem
	{
		public int DishId { get; set; }
		public int Qty { get; set; }
		public string? DishName { get; set; }

	}
	public class CreateMenuViewModel
	{
	[BindProperty]
		public int Id { get; set; } 
		public List<MenuItem> Items { get; set; } = new List<MenuItem>();
	}
}
