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
				.Include(p => p.PartyType);
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
                Location= party.Location,
                Status = party.Status!,
                HasMenu = party.HasMenu
            };

            var invoice = await _context.Invoices.Include(iv => iv.DetailInvoices).ThenInclude(di => di.Dish).FirstOrDefaultAsync(iv => iv.PartyId == party.PartyId);
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
				.Include(p=>p.Customer)
				.Include(p=>p.Invoices)
				.ThenInclude(iv=>iv.DetailInvoices)
				.ThenInclude(di=>di.Dish)
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
			if ( !PartyExists(id))
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
					//Selected = false,
					Price = (int)d.Price,
				})
				.ToListAsync();

			ViewBag.MinQty = party!.Quantity;
			ViewBag.partyId = id;
		
			return View(items);
		}
		// POST
		[HttpPost]
		public async Task<IActionResult> CreateMenu(int id, List<MenuItem> items) { 
			Console.ForegroundColor = ConsoleColor.Yellow;

			Console.WriteLine("SEND {0}",id);
			if (ModelState.IsValid)
			{

				Console.WriteLine("VALID");
				foreach (var item in items)

					{
					if (item.Qty>0 )
					{
						Console.WriteLine(item.DishId.ToString()+"-"+item.DishName+"-"+item.Qty.ToString());
					}
				}
				return RedirectToAction("Details",  new {id = id});
			}
			Console.ResetColor();

			return RedirectToAction("Index");
		}
		// POST
		[HttpPost]
		public async Task<IActionResult> CreateMenu_Mini(int id,[FromBody] List<MiniMenuItem> items)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;

			Console.WriteLine("MINI SEND {0}", id);
			if (ModelState.IsValid)
			{

				Console.WriteLine("VALID");
				Console.WriteLine("count {0}",items.Count);
				Console.OutputEncoding = Encoding.UTF8;

				foreach (var item in items)				{
					Console.WriteLine(item.DishId.ToString() + "-" + item.DishName + "-" + item.Qty.ToString());
				}

				Console.ResetColor();
				var party = await _context.Parties.FirstOrDefaultAsync(p => p.PartyId.Equals(id));
				party.HasMenu = true;
				_context.Update(party);
				await _context.SaveChangesAsync();

				 CreateInvoice(id,items.ToImmutableArray());

				return Json(Ok());
					//Ok();
				//RedirectToAction("Details", new { id = id });
				//Json(Url.Action("Details", "Parties", new { id = id }));
			}
			Console.ResetColor();

			return RedirectToAction("Index");
		}


		public async Task<IActionResult> CreateInvoice(int id,  ImmutableArray<MiniMenuItem> items)
		{
			try
			{

			 _context.Add(new Invoice()
			 {
				 PartyId = id,
				 InvoiceDate = DateTime.Now,
			 });

			}
			catch (DbUpdateConcurrencyException) { 
				return Problem("Cannot add new invoice"); }

			var invoiceId = _context.Invoices.Where(p => p.PartyId == id).First().InvoiceId;
			var allDish =  _context.Dishes;


			foreach (var item in items.ToImmutableArray())
			{
				var dish = await allDish.Where(d=>d.DishId.Equals(item.DishId)).FirstAsync();
				try
				{

				await _context.AddAsync(new DetailInvoice()
				{
					InvoiceId = invoiceId,
					DishId = item.DishId,
					Number = item.Qty,
					Price = dish.Price,
					Amount = dish.Price * item.Qty,
				});
				}
				catch {
					return Problem("Cannot add new invoice_detail");
				}

			}

			try
			{
				await _context.SaveChangesAsync();
            }
			catch
			{
				return Problem("Error from saving changes");
			}
            TempData["SuccessMessage"] = "Chọn thực đơn thành công";
            return RedirectToAction("Details", new { id = id });
		}


	}
}
