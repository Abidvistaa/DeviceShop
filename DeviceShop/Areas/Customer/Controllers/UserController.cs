using DeviceShop.Data;
using DeviceShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            List<ApplicationUser> applicationUsers = _db.ApplicationUsers.ToList<ApplicationUser>();
            return Json(new { data = applicationUsers });
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult>  Edit(string id)
        {
            var user = await _db.ApplicationUsers.FindAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    var roleSave = await _userManager.AddToRoleAsync(user, "User");
                    TempData["save"] = "User has been created Successfully";
                    return RedirectToAction(nameof(Index));
                }
              
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }  
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _db.ApplicationUsers.FirstOrDefault(x => x.Id == user.Id);
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.UserName = user.UserName;
                var result = await _userManager.UpdateAsync(userInfo);
                if (result.Succeeded)
                {
                    TempData["edit"] = "User has been Updated";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {   
            var user = _db.ApplicationUsers.Where(x => x.Id == id).FirstOrDefault<ApplicationUser>();
            _db.Remove(user);
            _db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" });   
        }


        //For Lockout & Lockend
        //user.LockoutEnd = DateTime.Now.AddMonths(2);
        //user.LockoutEnd = DateTime.Now.AddDays(-1);
    }
}
