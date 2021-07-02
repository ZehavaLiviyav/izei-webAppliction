using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string TimeOrder { get; set; }
        public String Address { get; set; }
        public List<Product> MyProductList { get; set; }
        public double TotalPrice { get; set; }
    }
}
