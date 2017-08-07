using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using protvshows.Models;

namespace protvshows.Controllers
{
    public class TVshowsController : Controller
    {
        private readonly TVshowContext _context;

        public TVshowsController(TVshowContext context)
		{
			_context = context;
		}

        // GET: tvshows/
        public IActionResult Index()
        {
            ViewData["Title"] = "Все сериалы";
            //tvshowContext context = HttpContext.RequestServices.GetService(typeof(tvshowContext)) as tvshowContext;
            return View(_context.GetAlltvshows());
        }

		// GET: tvshows/short_name
		[HttpGet("tvshows/{short_name}")]
        public IActionResult GetTVshow(string short_name)
        {
            ViewData["Title"] = short_name;
            return View("tvshow", _context.Gettvshow(short_name));
        }
    }
}
