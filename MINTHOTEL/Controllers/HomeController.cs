using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using MINTHOTEL.Models;
using System.Diagnostics;
using System.Security.Claims;



namespace MINTHOTEL.Controllers
{
	[Authorize]
   
   
    public class HomeController : Controller
	{
     
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
        
        public IActionResult loby()
		{
            return View();
        }

       
        public IActionResult Privacy()
		{
			return View();
		}

		
	}
}
