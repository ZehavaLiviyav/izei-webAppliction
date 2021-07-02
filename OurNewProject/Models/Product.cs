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
        public String Name { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ProductImage productImage { get; set; }
        public List<Order> MyOrderList { get; set; }

    }
}
