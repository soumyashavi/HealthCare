﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HealthCareEntities8 : DbContext
    {
        public HealthCareEntities8()
            : base("name=HealthCareEntities8")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NewForgotPaasword> NewForgotPaaswords { get; set; }
        public virtual DbSet<RequestAppointment3> RequestAppointment3 { get; set; }
    }
}