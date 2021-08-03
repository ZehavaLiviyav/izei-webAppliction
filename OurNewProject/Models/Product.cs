using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }
        [Display(Name = "שם")]
        [Required]
        public String Name { get; set; }
        [Required]
        [Display(Name = "מחיר")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "תיאור")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "קטגוריה")]
        public Category Category { get; set; }
        [Display(Name = "תמונה")]
        public ProductImage productImage { get; set; }
        [Display(Name = "הזמנות")]
        public List<Order> MyOrderList { get; set; }
       
    }
}
