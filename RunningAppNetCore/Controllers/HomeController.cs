using Microsoft.AspNetCore.Mvc;
using RunningAppNetCore.Models;
using System.Diagnostics;

namespace RunningAppNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Get: Index
        public IActionResult Index()
        {
            return View();
        }

        //Not Used
        public IActionResult Privacy()
        {
            return View();
        }

        //Get: Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}