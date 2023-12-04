using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin")]
    [Route("admin/dashboard")]

    public class DashboardController : Controller
    {
        private readonly QlDichVuNauTiecLanHueContext _context;

        public DashboardController(QlDichVuNauTiecLanHueContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _context.Parties.CountAsync();

                ViewData["PartyCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["PartyCount"] = 0;
            }

            try
            {
                var result = await _context.Customers.CountAsync();

                ViewData["CustomerCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["CustomerCount"] = 0;
            }

            try
            {
                var result = await _context.Staff.CountAsync();

                ViewData["StaffCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["StaffCount"] = 0;
            }

            try
            {
                var result = await _context.Dishes.CountAsync();

                ViewData["DishCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["DishCount"] = 0;
            }

            try
            {
                var result = await _context.Parties.CountAsync(p => p.Invoices.Any(i => i.PaymentTime == null));

                ViewData["NotPaidPartyCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["NotPaidPartyCount"] = 0;
            }

            try
            {
                var result = await _context.Parties.CountAsync(p => p.Status.Equals(Party.COMING_SOON));

                ViewData["ComingSoonPartyCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["ComingSoonPartyCount"] = 0;
            }

            try
            {
                var result = await _context.Parties.CountAsync(p => p.Status.Equals(Party.DONE));

                ViewData["DonePartyCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["DonePartyCount"] = 0;
            }

            try
            {
                var result = await _context.Parties.CountAsync(p => p.Status.Equals(Party.GOING_ON));

                ViewData["GoingOnPartyCount"] = result;
            }
            catch (Exception ex)
            {
                ViewData["GoingOnPartyCount"] = 0;
            }

            return View();
        }
    }
}