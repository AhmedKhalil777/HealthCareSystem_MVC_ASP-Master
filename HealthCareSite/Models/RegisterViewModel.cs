using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareSite.Models
{
    public class RegisterViewModel 
    {
     
       [Required]
       public string Name { get; set; }

       [Required]
       public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        Role role;


    }
}