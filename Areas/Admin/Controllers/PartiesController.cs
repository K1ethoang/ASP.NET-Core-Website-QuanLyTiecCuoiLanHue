using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Models;
using NuGet.Protocol;
using ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels;
using Microsoft.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using static ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.ViewModels.Menu;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http.HttpResults;
using Rotativa.AspNetCore;
using System.Collections;

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
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_Update_HappentStatus_In_Party");
            var qlDichVuNauTiecLanHueContext = _context.Parties
                .Include(p => p.Customer)
                .Include(p => p.PartyType).Include(p => p.Invoices);
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

            PartyDetailsViewModel viewModel = new PartyDetailsViewModel()
            {
                PartyId = party.PartyId,
                PartyName = party.PartyName,
                CustomerName = party.Customer.CusName!,
                Quantity = party.Quantity,
                PartyTypeName = party.PartyType.Name,
                Date = party.Date,
                Time = party.Time,
                Location = party.Location,
                Status = party.Status!,
                HasMenu = party.HasMenu
            };

            var invoice = await _context.Invoices
                .Include(iv => iv.DetailInvoices)
                .ThenInclude(di => di.Dish)
                .FirstOrDefaultAsync(iv => iv.PartyId == party.PartyId);
            if (invoice != null)
            {

                viewModel.DetailInvoices = invoice.DetailInvoices;

                Console.ForegroundColor = ConsoleColor.Green;
                //Console.WriteLine("DETAILS LIST: {0}",viewModel.DetailInvoices.ToJson());
                Console.ResetColor();
            }



            return View(viewModel);
        }

        // GET: Admin/Parties/Create
        public IActionResult Create()
        {
            PartyViewModel vm = new PartyViewModel();

            var customerSelectList = _context.Customers
                .ToList()
                .Select(c => new SelectListItem(
                    text: c.PhoneNumber + " - " + c.CusName,
                    value: Convert.ToString(c.CustomerId)))
                .ToList() ?? new List<SelectListItem>();

            ViewData["CustomerList"] = customerSelectList;

            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name");

            return View(vm);
        }

        // POST: Admin/Parties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
                                        [Bind("PartyId,PartyName,Quantity,Date,Time,Location,Note,PartyTypeId,CustomerId")]
                                        PartyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Party party = vm.ToParty(context: _context);
                _context.Add(party);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm thành công";
                return RedirectToAction(nameof(Index));
            }

            var customerSelectList = _context.Customers
                .ToList()
                .Select(c => new SelectListItem(
                    text: c.PhoneNumber + " - " + c.CusName,
                    value: Convert.ToString(c.CustomerId)))
                .ToList() ?? new List<SelectListItem>();

            ViewData["CustomerList"] = customerSelectList;

            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name", vm.PartyTypeId);

            return View(vm);
        }

        // GET: Admin/Parties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var party = await _context.Parties.FindAsync(id);

            if (party == null)
            {
                return NotFound();
            }

            if (party.Status.Equals(Party.DONE))
            {
                TempData["ErrorMessage1"] = "Tiệc đã tổ chức xong";
                return RedirectToAction(nameof(Index));
            }

            var customerSelectList = _context.Customers
                .ToList()
                .Select(c => new SelectListItem(
                    text: c.PhoneNumber + " - " + c.CusName,
                    value: Convert.ToString(c.CustomerId)))
                .ToList() ?? new List<SelectListItem>();

            PartyViewModel vm = new PartyViewModel(party);

            ViewData["CustomerList"] = customerSelectList;

            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name", party.PartyTypeId);

            return View(vm);
        }

        // POST: Admin/Parties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
                    [Bind("PartyName,Quantity,Date,Time,Location,Note,Status,HasMenu,PartyTypeId,CustomerId")]
                                            PartyViewModel vm)
        {
            var party = await _context.Parties.Where(p => p.PartyId == id).FirstAsync();

            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync(party.ToJson());
            Console.ResetColor();

            if (party == null || party.PartyId != id)
            {
                return NotFound();
            }

            _context.Attach(party);

            if (ModelState.IsValid)
            {
                try
                {
                    // You should use the Entry() method in case you want to update all the fields in your object.
                    // Also keep in mind you cannot change the field id(key) therefore first set the Id to the same as you edit.
                    // https://stackoverflow.com/a/47340877/20423795

                    _context.Entry(party).State = EntityState.Modified;
                    var newParty = vm.ToParty(context: _context);
                    newParty.PartyId = party.PartyId;
                    _context.Entry(party).CurrentValues.SetValues(newParty);
                    //_context.Update(party);
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
                TempData["SuccessMessage"] = "Lưu thành công";
                return RedirectToAction(nameof(Index));
            }

            var customerSelectList = _context.Customers
                .ToList()
                .Select(c => new SelectListItem(
                    text: c.PhoneNumber + " - " + c.CusName,
                    value: Convert.ToString(c.CustomerId)))
                .ToList<SelectListItem>() ?? new List<SelectListItem>();

            ViewData["CustomerList"] = customerSelectList;

            ViewData["PartyTypeId"] = new SelectList(_context.PartyTypes, "PartyTypeId", "Name", party.PartyTypeId);
            return View(vm);
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
            TempData["SuccessMessage"] = "Xoá thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool PartyExists(int id)
        {
            return (_context.Parties?.Any(e => e.PartyId == id)).GetValueOrDefault();
        }
        //[ActionName("Invoice")]
        //[Area("Admin/parties")]
        [Route("/invoice/{id}")]
        public async Task<IActionResult> Get_Invoice(int id)
        {
            if (!PartyExists(id))
            {
                return RedirectToAction(nameof(Index));
            }
            var party = _context.Parties?
                .Include(p => p.Customer)
                .Include(p => p.Invoices)
                .ThenInclude(iv => iv.DetailInvoices)
                .ThenInclude(di => di.Dish)
                .FirstOrDefault(e => e.PartyId == id);

            InvoiceDetailsViewModel viewModel = new InvoiceDetailsViewModel()
            {
                PartyId = id,
                Party = party,
                Invoice = party.Invoices.FirstOrDefault(),
                InvoiceId = party.Invoices.FirstOrDefault().InvoiceId,
                DetailInvoices = party.Invoices.FirstOrDefault().DetailInvoices,
                Customer = party.Customer,
            };

            return View(viewModel);
        }
        // GET
        [HttpGet]
        public async Task<IActionResult> CreateMenu(int id)
        {
            if (!PartyExists(id))
            {
                return NotFound();
            }

            var party = _context.Parties.Where(p => p.PartyId == id).FirstOrDefault();


            if (party.HasMenu
                //|| _context.Invoices.Any(iv=>iv.PartyId == id)
                )
            {
                TempData["ErrorMessage1"] = "Đã có thực đơn";
                return RedirectToAction(nameof(Index));
            }

            ViewData["DateAndTime"] = party!.Date.ToString();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(id);
            Console.ResetColor();

            List<MenuItem> items = await _context.Dishes
                .Select(d => new MenuItem()
                {
                    DishId = d.DishId,
                    DishName = d.DishName!,
                    UnitName = d.Unit.UnitName,
                    DishType = d.DishType.TypeName,
                    Qty = 0,
                    Selected = false,
                    Price = (int)d.Price,
                })
                .ToListAsync();

            ViewBag.MinQty = party!.Quantity;
            ViewBag.partyId = id;

            return View(items);
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> CreateMenu(int id, List<MenuItem> items)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("SEND {0}", id);
            if (ModelState.IsValid)
            {

                Console.WriteLine("VALID");
                foreach (var item in items)

                {
                    if (item.Qty > 0)
                    {
                        Console.WriteLine(item.DishId.ToString() + "-" + item.DishName + "-" + item.Qty.ToString());
                    }
                }
                return RedirectToAction("Details", new { id = id });
            }
            Console.ResetColor();

            return RedirectToAction("Index");
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> CreateMenu_Mini(int id, [FromBody] List<MiniMenuItem> items)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("MINI SEND {0}", id);

            if (ModelState.IsValid)
            {

                Console.WriteLine("VALID");
                Console.WriteLine("count {0}", items.Count);



                if (!items.Any())
                {
                    Console.WriteLine("NO ITEMS");
                    return Problem("NO ITEMS");
                }
                await CreateInvoice(id, items);


                return Json(Ok());
            }

            Console.ResetColor();
            return RedirectToAction("Index");
        }


        public async Task CreateInvoice(int id, ICollection<MiniMenuItem> items)
        {
            try
            {
                if (!_context.Invoices.Any(p => p.PartyId == id))
                {
                    await _context.AddAsync(new Invoice()
                    {
                        PartyId = id,
                        InvoiceDate = DateTime.Now,
                    });
                    await Console.Out.WriteLineAsync("INVOICE CREATED");
                    return;
                }

                var invoice = await _context.Invoices.FirstOrDefaultAsync(iv => iv.PartyId == id);

                if (invoice == null)
                {
                    await Console.Out.WriteLineAsync("INVOICE NOT FOUND");
                    return;
                }
                var invoiceId = invoice!.InvoiceId;

                Console.ForegroundColor = ConsoleColor.Yellow;
                ICollection<DetailInvoice> di_list = items.Select(item =>
                {
                    Console.WriteLine(item.DishId.ToString() + "-" + item.DishName + "-" + item.Qty.ToString());

                    return new DetailInvoice()
                    {
                        InvoiceId = invoiceId,
                        DishId = item.DishId,
                        Number = item.Qty,
                        Price = item.Price,
                        Amount = item.Price * item.Qty,
                    };
                })
                .ToImmutableArray();

                Task add_list = _context.DetailInvoices.AddRangeAsync(di_list);

                var party = await _context.Parties.FirstOrDefaultAsync(p => p.PartyId.Equals(id));
                party!.HasMenu = true;
                Task.WaitAny(add_list);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
            Console.ResetColor();

        }

        public async Task<IActionResult> ThanhToan([FromBody] int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC proc_CapNhatTienConLai " + id);
            await Console.Out.WriteLineAsync("INVOICE ID" + id);
            return Json(Ok());
        }
        public async Task<IActionResult> DatCoc([FromBody] int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC proc_CapNhatTienDatCoc " + id);
            Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync("DATCOC" + id);
            Console.ResetColor();
            return Json(Ok());
        }

        public async Task<IActionResult> Export_Invoice(int id)
        {
            var party = _context.Parties?
                .Include(p => p.Customer)
                .Include(p => p.Invoices)
                .ThenInclude(iv => iv.DetailInvoices)
                .ThenInclude(di => di.Dish)
                .FirstOrDefault(e => e.PartyId == id);

            InvoiceDetailsViewModel viewModel = _context.Invoices.Include(dv => dv.DetailInvoices)
                .Where(v => v.InvoiceId == id)
                .Select(v => new InvoiceDetailsViewModel()
                {
                    PartyId = id,
                    Party = party,
                    Invoice = party.Invoices.FirstOrDefault(),
                    InvoiceId = party.Invoices.FirstOrDefault().InvoiceId,
                    DetailInvoices = party.Invoices.FirstOrDefault().DetailInvoices,
                    Customer = party.Customer,
                }).FirstOrDefault();
            return new ViewAsPdf("Export_Invoice", viewModel)
            {
                FileName = $"invoice {viewModel.InvoiceId}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        // GET
        /*
         * @param id: party id
         */
        public IActionResult EditMenu(int id)
        {
            // get invoice id
            var foundInvoice = _context.Invoices.Where(inv => inv.PartyId == id).FirstOrDefault();

            if (foundInvoice == null)
            {
                return RedirectToAction(nameof(Index));
            }

            int invoiceId = foundInvoice.InvoiceId;

            // get items
            List<MiniMenuItem> items = _context.DetailInvoices
                .Where(di => di.InvoiceId == invoiceId)
                .Include(di => di.Dish)
                .ThenInclude(di => di.Unit)
                .Select(di => new MiniMenuItem
                {
                    DishId = di.DishId,
                    Qty = di.Number.GetValueOrDefault(),
                    DishName = di.Dish.DishName,
                    Price = (int)di.Price,
                })
                .ToList();
            // new view model
            EditMenuViewModel model = new EditMenuViewModel
            {
                InvoiceId = invoiceId,
                PartyId = id,
                OldMenu = items,
                NewMenu = items,
            };
            // find intersection
            IEnumerable<int> checkedList = items.Select(i => i.DishId);
            // view data
            ViewData["CheckedList"] = checkedList;
            ViewData["Repository"] = _context.Dishes
                .Include(i => i.Unit)
                .Include(i => i.DishType)
                .Select(i => new MenuItem
                {
                    DishId = i.DishId,
                    DishName = i.DishName,
                    Price = (int)i.Price,
                    DishType = i.DishType.TypeName,
                    UnitName = i.Unit.UnitName,
                    Selected = checkedList.Contains(i.DishId)
                })
                .OrderBy(i => i.Selected == false)
                .ToImmutableArray();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenu(EditMenuViewModel model)
        {

            //await Parallel.ForEachAsync(model.OldMenu, (item, token)=> { Console.WriteLine($"DishId = {item.DishId}; Qty = {item.Qty}");  });
            foreach(var item in model.OldMenu) Console.WriteLine($"DishId = {item.DishId}; Qty = {item.Qty}");
            foreach(var item in model.NewMenu) Console.WriteLine($"DishId = {item.DishId}; Qty = {item.Qty}");

            var compareNewToOld = model.OldMenu.Except(model.NewMenu);
            var compareOldToNew = model.NewMenu.Except(model.OldMenu);

            if (!(compareNewToOld.Any() && compareOldToNew.Any()))
            {
                Console.WriteLine("NO CHANGES");
                return RedirectToAction("Index");
            }

			List<MiniMenuItem> changes = compareNewToOld.Concat(compareOldToNew).ToList();
			List<MiniMenuItem> newEntries = model.OldMenu.Where(i => changes.All(_i => _i.DishId != i.DishId)).ToList();
			if (newEntries.Any())
			{
				changes.RemoveAll(i => newEntries.Contains(i));
			}
			// list of dishIDs from changes in ascending order
			ImmutableArray<int> changes_dishId = changes.Select(i => i.DishId).Order().ToImmutableArray();
			// create new detail_invoice for new entries, async
			Task addNewEntries = CreateInvoiceDetailsRange(model.InvoiceId, newEntries);
			// get entity-type entries to be updated
			var UpdatingDetailInvoices = _context.DetailInvoices
				.Where(i => i.InvoiceId == model.InvoiceId && changes_dishId.Contains(i.DishId))
				.AsTracking();

			foreach (var entry in UpdatingDetailInvoices)
			{
                if (!changes.Any()) { break; }

                MiniMenuItem item = changes.FirstOrDefault(i => i.DishId == entry.DishId);

                if (item == null) { break; }

                entry.Number = item.Qty;
                entry.Price = item.Price;
                entry.Amount = item.Price * item.Qty;
            }

            Task.WaitAll(addNewEntries);

            await _context.SaveChangesAsync();

            return View();
        }

        private async Task CreateInvoiceDetailsRange(int id, ICollection<MiniMenuItem> items)
        {
            ICollection<DetailInvoice> di_list = items.Select(item => new DetailInvoice()
            {
                InvoiceId = id,
                DishId = item.DishId,
                Number = item.Qty,
                Price = item.Price,
                Amount = item.Price * item.Qty,
            })
            .ToImmutableArray();

            await _context.DetailInvoices.AddRangeAsync(di_list);
            await _context.SaveChangesAsync();
        }
    }
}
