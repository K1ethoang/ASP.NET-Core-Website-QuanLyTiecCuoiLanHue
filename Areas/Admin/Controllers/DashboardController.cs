using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                // Execute the stored procedure and obtain a result
                var result = await _context.Parties
                    .FromSqlRaw("EXEC GetPartyCount")
        .FirstOrDefaultAsync();

                // Extract the first item from the result (assuming GetPartyCount returns a single integer)
                ViewData["PartyCount"] = result;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                ViewData["PartyCount"] = 0;
            }

            return View();
        }
    }
}