using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels;
using System.Collections;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CustomersController : Controller
    {
        private readonly QlDichVuNauTiecLanHueContext _context;

        public CustomersController(QlDichVuNauTiecLanHueContext context)
        {
            _context = context;
        }

        // GET: Admin/Customers
        [Route("admin/customers")]
        public async Task<IActionResult> Index()
        {
            return _context.Customers != null ?
                        View(await _context.Customers.ToListAsync()) :
                        Problem("Entity set 'QlDichVuNauTiecLanHueContext.Customers'  is null.");
        }

        // GET: Admin/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customers/Create
        public IActionResult Create()
        {
            CustomerViewModel vm = new CustomerViewModel();

            return View(vm);
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CusName,PhoneNumber,Sex,Address,CitizenNumber")] CustomerViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Customer customer = vm.ToCustomer(_context);

                customer.CusName = customer.CusName.Trim();
                customer.Address = customer.Address.Trim();

                bool existCusWithPhoneNum = CustomerExists(customer.PhoneNumber);
                bool existCusWithCitizenNum = CustomerExistsWithCitizenNum(customer.CitizenNumber);

                if (!existCusWithPhoneNum && !existCusWithCitizenNum)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm thành công";
                    return RedirectToAction(nameof(Index));
                }
                if (existCusWithPhoneNum)
                    TempData["ErrorMessage"] = "Số điện thoại đã có";
                if (existCusWithCitizenNum)
                    TempData["ErrorMessage1"] = "Số CCCD đã có";
            }
            return View(vm);
        }

        // GET: Admin/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            CustomerViewModel vm = new CustomerViewModel(customer);

            if (customer == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CusName,PhoneNumber,Sex,Address,CitizenNumber")] CustomerViewModel vm)
        {
            var customer = await _context.Customers.Where(c => c.CustomerId == id).FirstAsync();

            if (customer == null || id != customer.CustomerId)
            {
                return NotFound();
            }

            _context.Attach(customer);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(customer).State = EntityState.Modified;
                    var newCustomer = vm.ToCustomer(context: _context);
                    newCustomer.CustomerId = customer.CustomerId;
                    _context.Entry(customer).CurrentValues.SetValues(newCustomer);
                    _context.Update(customer);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Lưu thành công";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Có lỗi khi lưu";
            return View(vm);
        }

        // GET: Admin/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'QlDichVuNauTiecLanHueContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xoá thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

        private bool CustomerExists(string phoneNumber)
        {
            return (_context.Customers?.Any(e => e.PhoneNumber == phoneNumber)).GetValueOrDefault();
        }

        private bool CustomerExistsWithCitizenNum(string citizenNumber)
        {
            return (_context.Customers?.Any(e => e.CitizenNumber == citizenNumber)).GetValueOrDefault();
        }
    }
}
