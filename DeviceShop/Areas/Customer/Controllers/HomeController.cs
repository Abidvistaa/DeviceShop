using DeviceShop.Data;
using DeviceShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        private readonly ILogger<HomeController> _logger;

        

        public IActionResult Index()
        {
            var product = _db.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).ToList();
            return View(product);
        }
        public ActionResult Details(int id)
        {
            var product = _db.Products.Include(c => c.ProductType).Include(d => d.SpecialTag).FirstOrDefault(x => x.Id == id);
            return View(product);
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
