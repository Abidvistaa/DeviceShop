using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Areas.Admin.Models
{
    public class UserRoleMappingVM
    {
        
        [Display(Name ="User Id")]
        public string UserId { get; set; }
        
        [Display(Name = "Role Id")]
        public string RoleId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
