using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareSite.Models
{
    [Flags]
    enum Role
    {
        Admin =8 , Doctor=1, User=2, Anonymous=0
    }
    public class LoginViewModel 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }


    }
}