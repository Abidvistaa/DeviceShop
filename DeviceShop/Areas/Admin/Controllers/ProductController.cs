using DeviceShop.Data;
using DeviceShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace DeviceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {

            return View();
        }
        public ActionResult Details(int id)
        {
            var dbFromObj = _db.Products.Find(id);
            return View(dbFromObj);
        }

        //Create Get action Method

        public ActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(),"Id","ProductsType");
            ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "TagName");
            return View();
        }


        public ActionResult GetAll()
        {
            List<Product> products = _db.Products.Include(c => c.ProductType).Include(d => d.SpecialTag).ToList< Product>();
            return Json(new { data = products });
        }

        //Edit Get action Method
        public ActionResult Edit(int id)
        {
            var dbFromObj = _db.Products.Find(id);
            return View(dbFromObj);
        }


        //Create Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image!=null)
                {
                    var name = Path.Combine(_he.WebRootPath+"/images",Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "images/"+image.FileName;
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //TempData["save"] = "Saved Succeessfully";
            }
            return View(product);

        }


        //Edit Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {

            Product product = _db.Products.Where(x => x.Id == id).FirstOrDefault<Product>();
            _db.Products.Remove(product);
            _db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" });

        }
    }
}
