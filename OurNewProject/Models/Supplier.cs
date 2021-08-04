using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[א-ת ]+$", ErrorMessage = "שם הספק חייב להיות אותיות בעברית בלבד ")]

        [Display(Name = "שם")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9\s]*$")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "מספר טלפון חייב להיות 10 מספרים")]
        [Display(Name = "מספר טלפון")]
        public String Phone { get; set; }
        [Display(Name = "סניפים")]
        public List<Branch> myBranches { get; set; }
    }


}
