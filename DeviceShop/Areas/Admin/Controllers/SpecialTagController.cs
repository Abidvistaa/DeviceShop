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
            
            return View();
        }
        public ActionResult Details(int id)
        {
            var specialTag = _db.SpecialTags.Find(id);
            return View(specialTag);
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
            var specialTag = _db.SpecialTags.Find(id);
            return View(specialTag);
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
            
            var specialTag= _db.SpecialTags.Where(x=>x.Id==id).FirstOrDefault<SpecialTag>();
            _db.Remove(specialTag);
            _db.SaveChanges();
            return Json(new{success=true,message="Deleted Successfully" });
            
        }
    }
}
