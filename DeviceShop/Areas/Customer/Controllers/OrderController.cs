using DeviceShop.Data;
using DeviceShop.Models;
using Microsoft.AspNetCore.Http;
using DeviceShop.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DeviceShop.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get Checkout 

        public IActionResult Checkout()
        {
            return View();
        }

        //Post Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ProductId = product.Id;
                    order.OrderDetail.Add(orderDetail);
                }
            }
            order.OrderNo = GetorderNo();
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Product>());
            TempData["save"] = "Order has been successfull";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public string GetorderNo()
        {
            int rowCount = _db.Orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
