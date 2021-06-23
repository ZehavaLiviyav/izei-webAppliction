using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{
    public class BestSeller
    {
        public Category Category { get; set; }
        public List<Product> Products { get; set; }
    }
}
