using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UnitsController : Controller
    {
        private readonly QlDichVuNauTiecLanHueContext _context;

        public UnitsController(QlDichVuNauTiecLanHueContext context)
        {
            _context = context;
        }

        // GET: Admin/Units
        [Route("admin/units")]
        public async Task<IActionResult> Index()
        {
              return _context.Units != null ? 
                          View(await _context.Units.ToListAsync()) :
                          Problem("Entity set 'QlDichVuNauTiecLanHueContext.Units'  is null.");
        }

        // GET: Admin/Units/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Units == null)
        //    {
        //        return NotFound();
        //    }

        //    var unit = await _context.Units
        //        .FirstOrDefaultAsync(m => m.UnitId == id);
        //    if (unit == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(unit);
        //}

        // GET: Admin/Units/Create
        public IActionResult Create()
        {
            UnitViewModel vm = new UnitViewModel();
            return View(vm);
        }

        // POST: Admin/Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,UnitName,Description")] UnitViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Unit unit = vm.ToUnit(_context);
                unit.UnitName = unit.UnitName.Trim();

                bool existUnit = UnitExists(unit.UnitName);

                if (!existUnit)
                {
                    _context.Add(unit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Tên đơn vị đã có";

            }
            return View(vm);
        }

        // GET: Admin/Units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units.FindAsync(id);
            UnitViewModel vm = new UnitViewModel(unit);

            if (unit == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Admin/Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitId,UnitName,Description")] UnitViewModel vm)
        {
            var unit = await _context.Units.Where(d => d.UnitId == id).FirstAsync();

            if (id != unit.UnitId)
            {
                return NotFound();
            }

            _context.Attach(unit);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(unit).State = EntityState.Modified;
                    var newUnit = vm.ToUnit(context: _context);
                    newUnit.UnitId = unit.UnitId;
                    _context.Entry(unit).CurrentValues.SetValues(newUnit);
                    _context.Update(unit);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.UnitId))
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
            return View(vm);
        }

        // GET: Admin/Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .FirstOrDefaultAsync(m => m.UnitId == id);
            

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Admin/Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Units == null)
            {
                return Problem("Entity set 'QlDichVuNauTiecLanHueContext.Units'  is null.");
            }
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(int id)
        {
          return (_context.Units?.Any(e => e.UnitId == id)).GetValueOrDefault();
        }

        private bool UnitExists(string name)
        {
            return (_context.Units?.Any(e => e.UnitName == name)).GetValueOrDefault();
        }
    }
}
