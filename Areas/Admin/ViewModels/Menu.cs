using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using static ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels.Menu;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels
{
    public class Menu
    {
        public int PartyId { get; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public List<MenuItem> AvailableItems { get; set; }
        int MinQty;

        // CONSTRUCTORS
        public Menu() {
           
        }
        private void initAvailableItems(QlDichVuNauTiecLanHueContext context)
        {
            

            AvailableItems = context.Dishes
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
        private void  initMinQty (QlDichVuNauTiecLanHueContext context)
        {
            MinQty = context.Parties.Where(p=>p.PartyId==PartyId).First().Quantity ?? 0;
        }
        public Menu(int PartyId, QlDichVuNauTiecLanHueContext context) 
        {
            initAvailableItems(context);
            this.PartyId = PartyId;
        }
        public Menu(Party party, QlDichVuNauTiecLanHueContext context)
        {
            initAvailableItems(context);
            PartyId = party.PartyId;

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
            public int DishId { get => _DishId; set => _DishId = value; }

            //[DisplayName("Tên món ăn")]
            public string DishName;
            public string UnitName;
            public string DishType;
            [DisplayName("Số lượng")]
            public int Qty { get; set; }

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
    }
}
