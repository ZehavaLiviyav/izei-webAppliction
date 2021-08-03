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
        [Required]
        [Display(Name = "מוצר")]
        public int productId { get; set; }
        [Display(Name = "מוצר")]
        public Product product { get; set; }
        [Required]
        [Display(Name = "תמונה")]
        public string Imge { get; set; }

    }
}
