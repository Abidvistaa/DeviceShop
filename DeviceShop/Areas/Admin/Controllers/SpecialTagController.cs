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
    public class SpecialTagController : Controller
    {
        private ApplicationDbContext _db;

        public SpecialTagController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View(_db.SpecialTags.ToList());
        }
        public ActionResult Details(int id)
        {
            var dbFromObj = _db.SpecialTags.Find(id);
            return View(dbFromObj);
        }
        public ActionResult GetAll()
        {
            List<SpecialTag> specialTags = _db.SpecialTags.ToList<SpecialTag>();
            return Json(new { data = specialTags });
        }

        //Create Get action Method

        public ActionResult Create()
        {
            return View();
        }

        //Edit Get action Method
        public ActionResult Edit(int id)
        {
            var dbFromObj = _db.SpecialTags.Find(id);
            if (dbFromObj == null)
            {
                return NotFound();
            }
            return View(dbFromObj);
        }
       

        //Create Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }


        //Edit Post action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Update(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }

       
        

        [HttpPost]
        public ActionResult Delete(int id)
        {
            
            SpecialTag specialTag= _db.SpecialTags.Where(x=>x.Id==id).FirstOrDefault<SpecialTag>();
            _db.SpecialTags.Remove(specialTag);
            _db.SaveChanges();
            return Json(new{success=true,message="Deleted Successfully" });
            //var objFromDB = _db.ProductTypes.Find(id);
            //if (objFromDB == null)
            //{
            //    return Json(new { success = false, message = "Error While Deleting" });
            //}
            //_db.ProductTypes.Remove(objFromDB);
            //_db.SaveChanges();
            //return Json(new { success = true, message = "Delete Success" });
        }
    }
}
