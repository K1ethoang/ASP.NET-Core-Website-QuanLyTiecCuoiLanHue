﻿using System;
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

			return View(party);
		}

		// GET: Admin/Parties/Create
		public IActionResult Create()
		{
			PartyViewModel vm = new PartyViewModel();

			var customerSelectList = new List<SelectListItem>();

			var customerList = _context.Customers.ToList();

			customerSelectList.Add(new SelectListItem(
				   text: "",
				   value: ""
				   ));

			foreach (var cus in customerList)
			{
				customerSelectList.Add(new SelectListItem(
					text: cus.PhoneNumber + " - " + cus.CusName,
					value: Convert.ToString(cus.CustomerId)
					));
			}

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
				return RedirectToAction(nameof(Index));
			}

			var customerSelectList = new List<SelectListItem>();

			var customerList = _context.Customers.ToList();

			foreach (var cus in customerList)
			{
				customerSelectList.Add(new SelectListItem(
					text: cus.PhoneNumber + " - " + cus.CusName,
					value: Convert.ToString(cus.CustomerId)
					))
				;
			}

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

			//var customerSelectList = new List<SelectListItem>();

			//var customerList = _context.Customers.ToList();

			//foreach (var cus in customerList)
			//{
			//	customerSelectList.Add(new SelectListItem(
			//		text: cus.PhoneNumber + " - " + cus.CusName,
			//		value: Convert.ToString(cus.CustomerId)
			//		))
			//	;
			//}

			var customerSelectList = _context.Customers
						.ToList()
						.Select(c => new SelectListItem(
						text: c.PhoneNumber + " - " + c.CusName,
						value: Convert.ToString(c.CustomerId)
						))
						.ToList() ?? new List<SelectListItem>();

			PartyViewModel vm = new PartyViewModel(party);

			Console.ForegroundColor = ConsoleColor.Red;
			await Console.Out.WriteLineAsync(vm.ToJson());
			Console.ResetColor();

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
			var party = await _context.Parties.FindAsync(keyValues: id);

			if (party == null)
			{
				return NotFound();
			}

			Console.ForegroundColor = ConsoleColor.Red;
            await Console.Out.WriteLineAsync(ModelState.IsValid.ToString());
			Console.ResetColor();

			if (ModelState.IsValid)
			{
                try
				{
					_context.Entry(party).State = EntityState.Modified;

					party!.PartyName = vm.PartyName.Trim();
					party!.CustomerId = vm.CustomerId;
					party!.PartyId = vm.PartyTypeId;
					party!.Date = vm.Date;
					party!.Time = vm.Time;
					party!.Quantity = vm.Quantity;
					party!.Location = vm.Location?.Trim();
					party!.Note = vm.Note?.Trim();
					// Foreign Objects
					party!.PartyType = _context.PartyTypes.Find(vm.PartyTypeId) ?? new PartyType();
					party!.Customer = _context.Customers.Find(vm.CustomerId) ?? new Customer();

					party = vm.ToParty(context: _context);

					_context.Update(party);
					// Save changes
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

			//var customerSelectList = new List<SelectListItem>();

			//var customerList = 1;


			var customerSelectList = _context.Customers
									.ToList()
									.Select(c => new SelectListItem(
									text: c.PhoneNumber + " - " + c.CusName,
									value: Convert.ToString(c.CustomerId)
									))
									.ToList<SelectListItem>() ?? new List<SelectListItem>();

			//foreach (var cus in customerList)
			//{
			//	customerSelectList.Add(new SelectListItem(
			//		text: cus.PhoneNumber + " - " + cus.CusName,
			//		value: Convert.ToString(cus.CustomerId)
			//		))
			//	;
			//}

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
			return RedirectToAction(nameof(Index));
		}

		private bool PartyExists(int id)
		{
			return (_context.Parties?.Any(e => e.PartyId == id)).GetValueOrDefault();
		}
	}
}
