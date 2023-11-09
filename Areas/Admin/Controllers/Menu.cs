
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using System.ComponentModel;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.Controllers
{
    public class Menu
    {
        public int PartyId { get; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();

        // CONSTRUCTORS
        public Menu(int PartyId)
        {
            this.PartyId = PartyId;
        }
        public Menu(Party party)
        {
            this.PartyId = party.PartyId;

            if (party.HasMenu) { }
        }

        bool DishExists(int id)
        {
            return Items.Any(i => i.DishId == id);
        }
        public class MenuItem
        {
            //
            // Type nesting
            // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/nested-types
            //
            Menu? FromMenu;
            int _DishId;
            [DisplayName("Tên món ăn")]
            public int DishId { get => _DishId; }
            [DisplayName("Số lượng")]
            int Qty { get; set; }

            public MenuItem() { }
            public MenuItem(DishViewModel vm) { }
            public MenuItem(Dish dish, int qty = 1)
            {
                _DishId = dish.DishId;
                Qty = qty;
            }
            public MenuItem(int dishId, int qty = 1)
            {
                _DishId = dishId;
                Qty = qty;
            }
        }

    }
}
