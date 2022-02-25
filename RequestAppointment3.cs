//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthCareproject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class RequestAppointment3
    {
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public string Diseases { get; set; }
        public string Symptoms { get; set; }
        public string AffectedArea { get; set; }
        public int patientid { get; set; }
        public Nullable<int> id { get; set; }
        [Display(Name = "Book Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
      
        public Nullable<System.DateTime> BookTime { get; set; }
        public string EmailId { get; set; }
    
        public virtual NewForgotPaasword NewForgotPaasword { get; set; }
    }
}