using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public Product product { get; set; }
        public string Imge { get; set; }



    }
}
