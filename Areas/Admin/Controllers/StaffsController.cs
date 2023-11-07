﻿using System;
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
    public class StaffsController : Controller
    {
        private readonly QlDichVuNauTiecLanHueContext _context;

        public StaffsController(QlDichVuNauTiecLanHueContext context)
        {
            _context = context;
        }

        // GET: Admin/Staffs
        [Route("admin/staffs")]
        public async Task<IActionResult> Index()
        {
            var qlDichVuNauTiecLanHueContext = _context.Staff.Include(s => s.StaffType).Include(s => s.Users);

            return View(await qlDichVuNauTiecLanHueContext.ToListAsync());
        }

        // GET: Admin/Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.StaffType)
                //.Include(s => s.Users)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Admin/Staffs/Create
        public IActionResult Create()
        {
            StaffViewModel vm = new StaffViewModel();

            ViewData["StaffTypeId"] = new SelectList(_context.StaffTypes, "StaffTypeId", "Name");
            //ViewData["UsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View(vm);
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,StaffName,PhoneNumber,Sex,Address,CitizenNumber,StaffTypeId")] StaffViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Staff staff = vm.ToStaff(context: _context);
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffTypeId"] = new SelectList(_context.StaffTypes, "StaffTypeId", "Name", vm.StaffTypeId);
            //ViewData["UsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id", vm.UsersId);
            return View(vm);
        }

        // GET: Admin/Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            StaffViewModel vm = new StaffViewModel(staff);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["StaffTypeId"] = new SelectList(_context.StaffTypes, "StaffTypeId", "Name", staff.StaffTypeId);
            //ViewData["UsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id", staff.UsersId);
            return View(vm);
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,StaffName,PhoneNumber,Sex,Address,CitizenNumber,StaffTypeId")] StaffViewModel vm)
        {
            if (id != vm.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vm.ToStaff(context: _context));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(vm.StaffId))
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
            ViewData["StaffTypeId"] = new SelectList(_context.StaffTypes, "StaffTypeId", "Name", vm.StaffTypeId);
            //ViewData["UsersId"] = new SelectList(_context.AspNetUsers, "Id", "Id", staff.UsersId);
            return View(vm);
        }

        // GET: Admin/Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staff == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.StaffType)
                //.Include(s => s.Users)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Admin/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staff == null)
            {
                return Problem("Entity set 'QlDichVuNauTiecLanHueContext.Staff'  is null.");
            }
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
          return (_context.Staff?.Any(e => e.StaffId == id)).GetValueOrDefault();
        }
    }
}