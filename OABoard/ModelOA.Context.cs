﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OABoard
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProductionEntities : DbContext
    {
        public ProductionEntities()
            : base("name=ProductionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Prod_NetTimeTable> Prod_NetTimeTable { get; set; }
        public virtual DbSet<Prod_RecordTable> Prod_RecordTable { get; set; }
        public virtual DbSet<Prod_StdYearlyTable> Prod_StdYearlyTable { get; set; }
        public virtual DbSet<Prod_TimeBreakQueueTable> Prod_TimeBreakQueueTable { get; set; }
        public virtual DbSet<Prod_TodayWorkTable> Prod_TodayWorkTable { get; set; }
        public virtual DbSet<Prod_TimeBreakTable> Prod_TimeBreakTable { get; set; }
        public virtual DbSet<Emp_SectionTable> Emp_SectionTable { get; set; }
        public virtual DbSet<OR_log> OR_log { get; set; }
    }
}
