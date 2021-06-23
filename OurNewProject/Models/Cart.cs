using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }

    }

    public class Product_Quantity
    {
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double FinalPrice { get; set; }
    }

    public class CartView
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public List<Product_Quantity> Product_Quantity { get; set; }
        [Required]
        public double FinalPrice { get; set; }
        [Required]
        public List<Product> Reccomended { get; set; }
    }
}
