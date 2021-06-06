using DeviceShop.Data;
using DeviceShop.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace DeviceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypeController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View(_db.ProductTypes.ToList());
        }

        //Create Get action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }


    }
}
