using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class Branch
    {

        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[א-ת ]+$", ErrorMessage = "שם הסניף חייבת להיות אותיות בעברית בלבד ")]
        [Display(Name = "שם")]
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "מספר טלפון")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9\s]*$")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "מספר טלפון חייב להיות 9 מספרים")]
        public String Phone { get; set; }
        [Required] 
        [Display(Name = "כתובת")]
        public String Address { get; set; }
        [Required]
        [Display(Name = "שעות פתיחה")]
        public String Hours { get; set; }
        [Display(Name = "ספקים")]
        public List<Supplier> mySupplier { get; set; }
    }
}
