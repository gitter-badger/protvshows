using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using protvshows.Models;

namespace protvshows.Controllers
{
    public class tvshowsController : Controller
    {
        // GET: tvshows/
        public IActionResult Index()
        {
            tvshowContext context = HttpContext.RequestServices.GetService(typeof(tvshowContext)) as tvshowContext;

            return View(context.GetAlltvshows());
        }

		// GET: tvshows/short_name
		[HttpGet("tvshows/{short_name}")]
        public IActionResult GetTVshow(string short_name)
        {
			tvshowContext context = HttpContext.RequestServices.GetService(typeof(tvshowContext)) as tvshowContext;
            var tvshow = context.Gettvshow(short_name);
            if (tvshow != null) { ViewData["Message"] = tvshow.title; } else { ViewData["Message"] = "Сериал не найден"; }
            return View("tvshow", tvshow);
        }
    }
}
