using DeviceShop.Data;
using DeviceShop.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
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
            var product = _db.Products.Include(c => c.ProductType).Include(d => d.SpecialTag).FirstOrDefault(x => x.Id == id);
            return View(product);
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
        public ActionResult Edit(int id, IFormFile image)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductsType");
            ViewData["tagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "TagName");
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            return View(product);
        }


        //Create Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "images/" + image.FileName;
                }
                else
                {
                    product.Image = "images/noimage.png";
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
        public async Task<ActionResult> Edit(int id,Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "images/" + image.FileName;
                }
                else
                {

                    //Product obj = _db.Products.Where(x => x.Id == id).FirstOrDefault<Product>();
                    //product.Image = obj.Image;
                }

                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //TempData["save"] = "Saved Succeessfully";
            }
            return View(product);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {

            var product = _db.Products.Where(x => x.Id == id).FirstOrDefault<Product>();
            _db.Remove(product);
            _db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" });

        }
    }
}
