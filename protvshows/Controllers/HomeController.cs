using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace protvshows.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

		public HomeController(IConfiguration config)
		{
			configuration = config;
		}

		public IActionResult Index()
        {
            //return View();
            return RedirectToAction("Index", "tvshows", new { area = "" });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
