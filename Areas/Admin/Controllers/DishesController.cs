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
	[Area("Admin")]
	public class DishesController : Controller
	{
		private readonly QlDichVuNauTiecLanHueContext _context;

		public DishesController(QlDichVuNauTiecLanHueContext context)
		{
			_context = context;
		}

		// GET: Admin/Dishes
		[Route("admin/dishes")]
		public async Task<IActionResult> Index()
		{
			var qlDichVuNauTiecLanHueContext = _context.Dishes.Include(d => d.DishType).Include(d => d.Unit);
			return View(await qlDichVuNauTiecLanHueContext.ToListAsync());
		}

		// GET: Admin/Dishes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Dishes == null)
			{
				return NotFound();
			}

			var dish = await _context.Dishes
				.Include(d => d.DishType)
				.Include(d => d.Unit)
				.FirstOrDefaultAsync(m => m.DishId == id);
			if (dish == null)
			{
				return NotFound();
			}

			return View(dish);
		}

		// GET: Admin/Dishes/Create
		public IActionResult Create()
		{
			DishViewModel vm = new DishViewModel();

			ViewData["DishTypeId"] = new SelectList(_context.DishTypes,"DishTypeId", "TypeName");
			ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "UnitName");

			return View("CreateDishView", vm);
		}

		// POST: Admin/Dishes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("DishId,DishName,Price,DishTypeId,UnitId")] DishViewModel vm)
		{
			if (ModelState.IsValid)
			{
				Dish dish = vm.ToDish(_context);
				_context.Add(dish);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["DishTypeId"] = new SelectList(_context.DishTypes, "DishTypeId", "TypeName", vm.DishTypeId);
			ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "UnitName", vm.UnitId);
			return View("CreateDishView", vm);
		}

		// GET: Admin/Dishes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Dishes == null)
			{
				return NotFound();
			}

			var dish = await _context.Dishes.FindAsync(id);
			if (dish == null)
			{
				return NotFound();
			}
			ViewData["DishTypeId"] = new SelectList(_context.DishTypes, "DishTypeId", "DishTypeId", dish.DishTypeId);
			ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "UnitId", dish.UnitId);
			return View(dish);
		}

		// POST: Admin/Dishes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("DishId,DishName,Price,DishTypeId,UnitId")] Dish dish)
		{
			if (id != dish.DishId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(dish);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!DishExists(dish.DishId))
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
			ViewData["DishTypeId"] = new SelectList(_context.DishTypes, "DishTypeId", "DishTypeId", dish.DishTypeId);
			ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "UnitId", dish.UnitId);
			return View(dish);
		}

		// GET: Admin/Dishes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Dishes == null)
			{
				return NotFound();
			}

			var dish = await _context.Dishes
				.Include(d => d.DishType)
				.Include(d => d.Unit)
				.FirstOrDefaultAsync(m => m.DishId == id);
			if (dish == null)
			{
				return NotFound();
			}

			return View(dish);
		}

		// POST: Admin/Dishes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Dishes == null)
			{
				return Problem("Entity set 'QlDichVuNauTiecLanHueContext.Dishes'  is null.");
			}
			var dish = await _context.Dishes.FindAsync(id);
			if (dish != null)
			{
				_context.Dishes.Remove(dish);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool DishExists(int id)
		{
			return (_context.Dishes?.Any(e => e.DishId == id)).GetValueOrDefault();
		}
	}
}
