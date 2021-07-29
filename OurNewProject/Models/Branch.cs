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
        [Display(Name = "שם")]
        public string Name { get; set; }
        [Display(Name = "מספר טלפון")]
        public String Phone { get; set; }
        [Display(Name = "כתובת")]
        public String Address { get; set; }
        [Display(Name = "שעות פתיחה")]
        public String Hours { get; set; }
        [Display(Name = "ספקים")]
        public List<Supplier> mySupplier { get; set; }
    }
}
