using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using protvshows.Models;

namespace protvshows.Controllers
{
    public class AdminController : Controller
    {
		private readonly TVshowContext _context;
		public AdminController(TVshowContext context)
		{
			_context = context;
		}

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

		// GET: /<controller>/add
		public IActionResult Add()
		{
			return View();
		}

		// POST: /<controller>/add
		//[HttpPost]
		//[ValidateAntiForgeryToken]
  //      public async Task<IActionResult> Create(TVshow newTVshow)
		//{
		//	if (ModelState.IsValid)
		//	{
  //              await _context.Create(newTVshow);
		//		return RedirectToAction("Index");
		//	}
  //          return View(newTVshow);
		//}
    }
}
