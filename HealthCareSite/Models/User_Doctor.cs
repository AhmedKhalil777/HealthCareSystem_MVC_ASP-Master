//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthCareSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Doctor
    {
        public string Interact_des { get; set; }
        public int User_User_ID { get; set; }
        public int Doctor_Doc_ID { get; set; }
        public string Report { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual User User { get; set; }
    }
}