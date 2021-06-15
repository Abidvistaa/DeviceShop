using DeviceShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Areas.Admin
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class ProductOrderController : Controller
    {
        ApplicationDbContext _db;
        public ProductOrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View(_db.Orders.ToList());
        }
    }
}
