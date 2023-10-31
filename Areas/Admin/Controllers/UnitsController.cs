using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Website_QuanLyTiecCuoiLanHue.Areas.Admin.Controllers
{
	public class UnitsController : Controller
	{
		// GET: UnitsController
		public ActionResult Index()
		{
			return View();
		}

		// GET: UnitsController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: UnitsController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UnitsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UnitsController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: UnitsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UnitsController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: UnitsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
