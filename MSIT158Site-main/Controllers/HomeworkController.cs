using Microsoft.AspNetCore.Mvc;
using MSIT158Site.Models;

namespace MSIT158Site.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly MyDBContext _context;
        public HomeworkController(MyDBContext context)
        {
            _context = context;
        }

        public IActionResult Homework1()
        {
            return View();
        }
        public IActionResult Homework2()
        {
            return View();
        }
        public IActionResult Homework3()
        {
            return View();
        }
        public IActionResult Homework4()
        {
            return View();
        }
    }
}
