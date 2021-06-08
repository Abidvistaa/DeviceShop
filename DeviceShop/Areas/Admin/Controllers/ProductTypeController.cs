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
            
            return View();
        }
        public ActionResult Details(int id)
        {
            var dbFromObj = _db.ProductTypes.Find(id);
            return View(dbFromObj);
        }

        //Create Get action Method

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult GetAll()
        {
            List<ProductType> productsList = _db.ProductTypes.ToList<ProductType>();
            return Json(new { data= productsList });
        }

        //Edit Get action Method
        public ActionResult Edit(int id)
        {
            var dbFromObj = _db.ProductTypes.Find(id);
            return View(dbFromObj);
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
                //TempData["save"] = "Saved Succeessfully";
            }
            
            return View(productType);
            
        }


        //Edit Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Update(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

       
        

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            ProductType productType= _db.ProductTypes.Where(x=>x.Id==id).FirstOrDefault<ProductType>();
            _db.ProductTypes.Remove(productType);
            _db.SaveChanges();
            return Json(new{success=true,message="Deleted Successfully" });
           
        }
    }
}
