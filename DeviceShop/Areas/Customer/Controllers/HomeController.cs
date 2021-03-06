using DeviceShop.Data;
using DeviceShop.Models;
using DeviceShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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
        
        
        public IActionResult Index(int? page)
        {
            var product = _db.Products.Include(x => x.ProductType).Include(y => y.SpecialTag).ToList().ToPagedList(page??1,8);
            return View(product);
        }
        
        public ActionResult Details(int? id)
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


        
        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductSession(int id)
        {
            var product = _db.Products.Include(c => c.ProductType).Include(d => d.SpecialTag).FirstOrDefault(x => x.Id == id);
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

       
        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            return View(products);
        }

        
        //Get Remove from Cart
        [ActionName("Remove")]
        public ActionResult RemoveCart(int id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            Product product = null;
            product = products.FirstOrDefault(c => c.Id == id);
            products.Remove(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Cart));
        }
    }
}
