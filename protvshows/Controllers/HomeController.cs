using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
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
		
        public IActionResult Error()
		{
            ViewData["Title"] = "Ошибка";
			
            //var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();

			//ViewData["statusCode"] = HttpContext.Response.StatusCode;
			//ViewData["message"] = exception.Error.Message;
			//ViewData["stackTrace"] = exception.Error.StackTrace;

            return View();
		}

		[HttpGet("/Home/StatusCode/{statusCode}")]
		public IActionResult Index(int statusCode)
        {
            //var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            //_logger.LogInformation($"Unexpected Status Code: {statusCode}, OriginalPath: {reExecute.OriginalPath}");
            return View("StatusCode", statusCode);
        }

		public IActionResult Index()
        {
            //ViewData["Title"] = "Главная страница";
            //return View();
            return RedirectToAction("Index", "tvshows", new { area = "" });
        }

        public IActionResult About()
        {
            ViewData["Title"] = "О проекте";
            return View();
        }

		public IActionResult Search()
		{
            ViewData["Title"] = "Поиск";
			int x = 0;
			int y = 5 / x;
			return View();
		}

		public IActionResult SuggestMe()
		{
            ViewData["Title"] = "Подбор сериала";
			return View();
		}
    }
}
