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
        [Display(Name = "שם")]
        public string Name { get; set; }
        [Display(Name = "מספר טלפון")]
        public String Phone { get; set; }
        [Display(Name = "סניפים")]
        public List<Branch> myBranches { get; set; }
    }


}
