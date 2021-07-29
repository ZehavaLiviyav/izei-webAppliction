using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OurNewProject.Models
{ 
    public enum UserType
        {
            Client,
            Admin
        }
        //
    public class User
    {
       
        [Key]
        public int Id { get; set; }


        [Required]
        [Display(Name = "שם")]
        public String UserName { get; set; }

        [Required]
        [Display(Name = "סיסמא")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

         [Display(Name = "טיפוס")]
        public UserType Type { get; set; } = UserType.Client;


    }
}
