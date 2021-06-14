using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Areas.Admin.Models
{
    public class UserRoleVM
    {
        [Required]
        [Display(Name ="User Id")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Role Id")]
        public string RoleId { get; set; }
    }
}
