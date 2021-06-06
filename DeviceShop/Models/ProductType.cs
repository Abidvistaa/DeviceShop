using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceShop.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Product Type")]
        public string ProductsType { get; set; }
    }
}
