using Microsoft.AspNetCore.Mvc;
using MSIT158Site.Models;
using System.Diagnostics;
using System.IO;

namespace MSIT158Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDBContext _context;

        public HomeController(ILogger<HomeController> logger, MyDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {          
            return View(_context.Categories);
        }

        public IActionResult First()
        {
            return View();
        }
        public IActionResult Address()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult JSONTest()
        {
            return View();
        }
        public IActionResult try1()
        {
            StreamReader sr = new StreamReader(@"C:\Users\User\Downloads\MSIT158Site-main\MSIT158Site-main\wwwroot\js\travel.js");
            string s = string.Empty;
            while (sr.ReadLine() != null)
                s += sr.ReadLine();
            sr.Close();

            return Json(s);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}