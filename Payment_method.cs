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
    
    public partial class Payment_method
    {
        public string NameOnCard { get; set; }
        public int CreditCardNumber { get; set; }
        public string ExpMonth { get; set; }
        public Nullable<int> CVV { get; set; }
        public Nullable<long> ExpYear { get; set; }
    }
}
