using DnD_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DnD_CMS.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        // Auto generated code from visual studio
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Get request for site Homepage
        public IActionResult Index()
        {
            return View();
        }

        // Get request for site About page
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Guide()
        {
            return View();
        }

        // Auto generated code from visual studio
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}