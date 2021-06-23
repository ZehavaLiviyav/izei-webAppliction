using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class ProductImage
    {   
        [Key]
        public int productId { get; set; }
        public Product product { get; set; }
            

    }
}
