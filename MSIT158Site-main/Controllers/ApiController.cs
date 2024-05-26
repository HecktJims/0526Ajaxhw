using Microsoft.AspNetCore.Mvc;
using MSIT158Site.Models;
using System.Text.Json;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace MSIT158Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment _webenv;
        public ApiController(MyDBContext context, IWebHostEnvironment webenv)
        {
            _context = context;
            _webenv = webenv;
        }

        public IActionResult Index()
        {
            Thread.Sleep(1000);
            return Content("世界, 您好!!","text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult Avatar(int id =1)
        {
            Member? member = _context.Members.Find(id);
            if(member != null)
            {
                byte[] img = member.FileData;
                if(img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
        public IActionResult hw1()
        {
            var spots = from s in _context.SpotImagesSpots select new { s.SpotTitle, s.SpotDescription, s.SpotImage, s.Address };
            return Json(spots);
        }
        public IActionResult hw2()
        {
            var categories = _context.Categories;
            var categories2 = _context.Categories.FirstOrDefault(x => x.CategoryName);
            return Json(categories);

            //var spots = from s in _context.Spots select s;
            //var imgs = from s in _context.SpotImages select s;
            //string word = JsonSerializer.Serialize(imgs);
            //List<string> list2 = JsonSerializer.Deserialize<List<string>>(word);

            //List<C_vmhw> list = new List<C_vmhw>();
            ////word = list2.ElementAt(0);
            //int i = 0;
            //foreach (var item in spots)
            //{
            //    C_vmhw x = new C_vmhw();

            //    x.title = item.SpotTitle;
            //    x.desc = item.SpotDescription.Substring(0, 20);
            //    x.pic = list2.ElementAt(i);

            //    i++;
            //    list.Add(x);
            //}
            //return Content(word);
        }
        public IActionResult getcity()
        {
            var citys = (from s in _context.Addresses select s.City).Distinct();
            return Json(citys);
        }
        public IActionResult getdist(string city)
        {
            int L = city.Length;
            var dists = (from s in _context.Addresses
                        where s.City == city
                        select s.SiteId.Substring(L)).Distinct();
            return Json(dists);
        }
        public IActionResult getroad(string city, string dist)
        { 
            string word = city + dist;
            var roads = from s in _context.Addresses
                        where s.SiteId == word
                        select s.Road;
            return Json(roads);
        }
        public IActionResult CheckAccount(string name)
        {
            bool isExist = _context.Members.Any(m => m.Name == name);
            if (isExist)
                return Content("1");
            return Content("0");
            //return Content(member.ToString(), "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult Register(C_vmHw3 vm)
        {
            return Json(vm);
            
            if (vm == null || vm.avatar.Length == 0)
                return NotFound();

            string s = "";
            s += $"姓名：{vm.name}  /  ";
            s += $"電子郵件：{vm.email}  /  ";
            s += $"密碼：{vm.psw}  /  ";
            s += $"年齡：{vm.age}  /  ";
            s += $"頭像：{vm.avatar.FileName}";

            string filename = DateTime.Now.ToString("MMdd") + vm.name + ".jpg";
            string filepath = Path.Combine(_webenv.WebRootPath, "images", filename);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                vm.avatar.CopyTo(stream);
            }

            return Content(s, "text/html", System.Text.Encoding.UTF8);
        }
    }
}
