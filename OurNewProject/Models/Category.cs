using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[א-ת ]+$", ErrorMessage = "שם הקטגוריה חייבת להיות אותיות בעברית בלבד ")]
        
        [Display(Name = "שם קטגוריה")]
        public string Name { get; set; }
        [Display(Name = "מוצרים")]
        public List<Product> myProducts { get; set; }
    }
}
