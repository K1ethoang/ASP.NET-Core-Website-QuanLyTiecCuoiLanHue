using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PartiesController : Controller
    {
        private readonly QlDichVuNauTiecLanHueContext _context;

        public PartiesController(QlDichVuNauTiecLanHueContext context)
        {
            _context = context;
        }

        // GET: Admin/Parties
        [Route("admin/parties")]
        public async Task<IActionResult> Index()
        {
            var qlDichVuNauTiecLanHueContext = _context.Parties.Include(p => p.Customer).Include(p => p.PartyType);
            return View(await qlDichVuNauTiecLanHueContext.ToListAsync());
        }

        // GET: Admin/Parties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parties == null)
            {
                return NotFound();
            }

            var party = await _context.Parties
                .Include(p => p.Customer)
                .Include(p => p.PartyType)
                .FirstOrDefaultAsync(m => m.PartyId == id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // GET: Admin/Parties/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CusName");
            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name");
            return View();
        }

        // POST: Admin/Parties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartyId,PartyName,Quantity,Date,Time,Location,Note,Status,HasMenu,PartyTypeId,CustomerId")] Party party)
        {
            if (ModelState.IsValid)
            {
                _context.Add(party);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CusName", party.CustomerId);
            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name", party.PartyTypeId);
            return View(party);
        }

        // GET: Admin/Parties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parties == null)
            {
                return NotFound();
            }

            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CusName", party.CustomerId);
            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name", party.PartyTypeId);
            return View(party);
        }

        // POST: Admin/Parties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartyId,PartyName,Quantity,Date,Time,Location,Note,Status,HasMenu,PartyTypeId,CustomerId")] Party party)
        {
            if (id != party.PartyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(party);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyExists(party.PartyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CusName", party.CustomerId);
            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name", party.PartyTypeId);
            return View(party);
        }

        // GET: Admin/Parties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parties == null)
            {
                return NotFound();
            }

            var party = await _context.Parties
                .Include(p => p.Customer)
                .Include(p => p.PartyType)
                .FirstOrDefaultAsync(m => m.PartyId == id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Admin/Parties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parties == null)
            {
                return Problem("Entity set 'QlDichVuNauTiecLanHueContext.Parties'  is null.");
            }
            var party = await _context.Parties.FindAsync(id);
            if (party != null)
            {
                _context.Parties.Remove(party);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartyExists(int id)
        {
          return (_context.Parties?.Any(e => e.PartyId == id)).GetValueOrDefault();
        }
    }
}
