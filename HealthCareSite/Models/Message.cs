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
    
    public partial class Message
    {
        public int Message_ID { get; set; }
        public int Message_index { get; set; }
        public string Message_Content { get; set; }
        public string Type { get; set; }
        public string Sender { get; set; }
        public int Group_Group_ID { get; set; }
    
        public virtual Group Group { get; set; }
    }
}