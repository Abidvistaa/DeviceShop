using DeviceShop.Areas.Admin.Models;
using DeviceShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AssignController : Controller
    {
        RoleManager<IdentityRole> _rm;
        UserManager<IdentityUser> _um;
        ApplicationDbContext _db;
        public AssignController(RoleManager<IdentityRole> rm, UserManager<IdentityUser> um, ApplicationDbContext db)
        {
            _rm = rm;
            _um = um;
            _db=db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_db.ApplicationUsers.ToList(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_rm.Roles.ToList(), "Name", "Name");

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(UserRoleVM vM)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == vM.UserId);
            //var user = await _um.FindByIdAsync(id);
            var isExist = await _um.IsInRoleAsync(user,vM.RoleId);
            if (isExist==true)
            {
                ViewBag.msg = "This user already assigned";
                return View();
            }
            var role = await _um.AddToRoleAsync(user, vM.RoleId);
            if (role.Succeeded)
            {

                TempData["save"] = "User Role has been assigned ";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }




        public IActionResult AssignUserRole()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join u in _db.ApplicationUsers on ur.UserId equals u.Id
                         select new UserRoleMapping()
                         {
                             UserId=ur.UserId,
                             RoleId=ur.RoleId,
                             UserName=u.UserName,
                             RoleName=r.Name
                         };
            ViewBag.UserRole = result;
            return View();
            //return Json(new { data = ViewBag.UserRole });
        }

        


       
    }
}
