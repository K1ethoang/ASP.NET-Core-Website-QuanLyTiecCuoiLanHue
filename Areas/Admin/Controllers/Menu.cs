
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

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
        }
        public class MenuItem 
        {
        //
        // Type nesting
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/nested-types
        //
            Menu? FromMenu;

            public MenuItem() { }
            public MenuItem(Menu? menu)
            {
                FromMenu = menu;
            }
        }

    }
}
