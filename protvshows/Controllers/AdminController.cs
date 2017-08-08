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
            ViewData["Title"] = "Все сериалы";
            return View(_context.GetAlltvshows());
        }

		// GET: /<controller>/add
		public IActionResult Add()
		{
            ViewData["Title"] = "Добавить новый сериал";
			return View();
		}

		// POST: /<controller>/add
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Add(TVshow newTVshow)
		{
			if (ModelState.IsValid)
			{
                try
                {
                    //throw new Exception("Да просто проверка исключений");
                    _context.Create(newTVshow);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewData["Exception"] = string.Format("{0}", ex.Message);
                }
			}
            ViewData["Title"] = "Добавить новый сериал";
            return View(newTVshow);
		}
    }
}
