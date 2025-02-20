﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProductionEntities11 : DbContext
    {
        public ProductionEntities11()
            : base("name=ProductionEntities11")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<App_ProductionToday> App_ProductionToday { get; set; }
        public virtual DbSet<App_ProductionTodayLog> App_ProductionTodayLog { get; set; }
        public virtual DbSet<Emp_AuthorizationTable> Emp_AuthorizationTable { get; set; }
        public virtual DbSet<Emp_DivisionTable> Emp_DivisionTable { get; set; }
        public virtual DbSet<Emp_ManPowerRegistedTable> Emp_ManPowerRegistedTable { get; set; }
        public virtual DbSet<Emp_ManPowersTable> Emp_ManPowersTable { get; set; }
        public virtual DbSet<Emp_MPFunctionTable> Emp_MPFunctionTable { get; set; }
        public virtual DbSet<Emp_Roles> Emp_Roles { get; set; }
        public virtual DbSet<Emp_SectionTable> Emp_SectionTable { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<division> divisions { get; set; }
        public virtual DbSet<Emp_DecIncNameTable> Emp_DecIncNameTable { get; set; }
        public virtual DbSet<Emp_PlantTable> Emp_PlantTable { get; set; }
        public virtual DbSet<Emp_SectionMemberTable> Emp_SectionMemberTable { get; set; }
        public virtual DbSet<Emp_UserLoginTable> Emp_UserLoginTable { get; set; }
        public virtual DbSet<Emp_UserLoginTrackingTable> Emp_UserLoginTrackingTable { get; set; }
        public virtual DbSet<Exclusion_ItemsTable> Exclusion_ItemsTable { get; set; }
        public virtual DbSet<Exclusion_RecordTable> Exclusion_RecordTable { get; set; }
        public virtual DbSet<exclusionItem> exclusionItems { get; set; }
        public virtual DbSet<exclusionRecord> exclusionRecords { get; set; }
        public virtual DbSet<GHOee_Raw> GHOee_Raw { get; set; }
        public virtual DbSet<Loss_DetailTable> Loss_DetailTable { get; set; }
        public virtual DbSet<Loss_ItemsTable> Loss_ItemsTable { get; set; }
        public virtual DbSet<Loss_RecordTable> Loss_RecordTable { get; set; }
        public virtual DbSet<Loss_SixGroupTable> Loss_SixGroupTable { get; set; }
        public virtual DbSet<Loss_SixRelatedTable> Loss_SixRelatedTable { get; set; }
        public virtual DbSet<Loss_SummaryTable> Loss_SummaryTable { get; set; }
        public virtual DbSet<machineFaultCode> machineFaultCodes { get; set; }
        public virtual DbSet<machineList> machineLists { get; set; }
        public virtual DbSet<manPowerDailyRegister> manPowerDailyRegisters { get; set; }
        public virtual DbSet<manPowerFunction> manPowerFunctions { get; set; }
        public virtual DbSet<manPowerOptionDI> manPowerOptionDIs { get; set; }
        public virtual DbSet<manPowerRegister> manPowerRegisters { get; set; }
        public virtual DbSet<manPower> manPowers { get; set; }
        public virtual DbSet<ML_MachineDeviceTable> ML_MachineDeviceTable { get; set; }
        public virtual DbSet<ML_NagaraStartTimeTable> ML_NagaraStartTimeTable { get; set; }
        public virtual DbSet<ML_RecordTable> ML_RecordTable { get; set; }
        public virtual DbSet<Oee_CategoryName> Oee_CategoryName { get; set; }
        public virtual DbSet<Oee_ItemName> Oee_ItemName { get; set; }
        public virtual DbSet<oeeBottleneck> oeeBottlenecks { get; set; }
        public virtual DbSet<oeeHistory> oeeHistorys { get; set; }
        public virtual DbSet<oeeItem> oeeItems { get; set; }
        public virtual DbSet<oeeMachine> oeeMachines { get; set; }
        public virtual DbSet<oeeMachineTime> oeeMachineTimes { get; set; }
        public virtual DbSet<oeeProductionLine> oeeProductionLines { get; set; }
        public virtual DbSet<oeeRelatedLoss> oeeRelatedLosses { get; set; }
        public virtual DbSet<orDeceiveguest> orDeceiveguests { get; set; }
        public virtual DbSet<orHistory> orHistorys { get; set; }
        public virtual DbSet<Pg_Loss> Pg_Loss { get; set; }
        public virtual DbSet<Pg_MH> Pg_MH { get; set; }
        public virtual DbSet<Pg_PostPlan> Pg_PostPlan { get; set; }
        public virtual DbSet<PG_Production> PG_Production { get; set; }
        public virtual DbSet<Pg_STDMH> Pg_STDMH { get; set; }
        public virtual DbSet<Prod_CustWorkingDayTable> Prod_CustWorkingDayTable { get; set; }
        public virtual DbSet<Prod_MachineCycleTimeTable> Prod_MachineCycleTimeTable { get; set; }
        public virtual DbSet<Prod_MachineFaultCodeTable> Prod_MachineFaultCodeTable { get; set; }
        public virtual DbSet<Prod_MachineNameTable> Prod_MachineNameTable { get; set; }
        public virtual DbSet<Prod_NetTimeTable> Prod_NetTimeTable { get; set; }
        public virtual DbSet<Prod_OABoardTable> Prod_OABoardTable { get; set; }
        public virtual DbSet<Prod_PPASbyPartNumberTable> Prod_PPASbyPartNumberTable { get; set; }
        public virtual DbSet<Prod_PPASVOTable> Prod_PPASVOTable { get; set; }
        public virtual DbSet<Prod_ProdPlanTable> Prod_ProdPlanTable { get; set; }
        public virtual DbSet<Prod_ProductionToday> Prod_ProductionToday { get; set; }
        public virtual DbSet<Prod_ProgressResultTable> Prod_ProgressResultTable { get; set; }
        public virtual DbSet<Prod_RecordTable> Prod_RecordTable { get; set; }
        public virtual DbSet<Prod_StdMonthlyTable> Prod_StdMonthlyTable { get; set; }
        public virtual DbSet<Prod_StdWorkingDayTable> Prod_StdWorkingDayTable { get; set; }
        public virtual DbSet<Prod_StdYearlyTable> Prod_StdYearlyTable { get; set; }
        public virtual DbSet<Prod_TimeBreakQueueTable> Prod_TimeBreakQueueTable { get; set; }
        public virtual DbSet<Prod_TimeBreakTable> Prod_TimeBreakTable { get; set; }
        public virtual DbSet<Prod_TodayWorkTable> Prod_TodayWorkTable { get; set; }
        public virtual DbSet<productionPlan> productionPlans { get; set; }
        public virtual DbSet<productionPpa> productionPpas { get; set; }
        public virtual DbSet<productionRecordEnd> productionRecordEnds { get; set; }
        public virtual DbSet<productionRecord> productionRecords { get; set; }
        public virtual DbSet<productionRedFront> productionRedFronts { get; set; }
        public virtual DbSet<productionTodayApproval> productionTodayApprovals { get; set; }
        public virtual DbSet<productionToday> productionTodays { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<progressLoss> progressLosses { get; set; }
        public virtual DbSet<progressMH> progressMHs { get; set; }
        public virtual DbSet<progressPostPlan> progressPostPlans { get; set; }
        public virtual DbSet<progressProduction> progressProductions { get; set; }
        public virtual DbSet<progressSTDMH> progressSTDMHs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<section> sections { get; set; }
        public virtual DbSet<servicePreparation> servicePreparations { get; set; }
        public virtual DbSet<Software_ErrorCodeTable> Software_ErrorCodeTable { get; set; }
        public virtual DbSet<Software_Info> Software_Info { get; set; }
        public virtual DbSet<Software_RevisionTable> Software_RevisionTable { get; set; }
        public virtual DbSet<Software_ServiceRevision> Software_ServiceRevision { get; set; }
        public virtual DbSet<standardNetTime> standardNetTimes { get; set; }
        public virtual DbSet<standardYearly> standardYearlys { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<timeBreak> timeBreaks { get; set; }
        public virtual DbSet<timeBreakUsing> timeBreakUsings { get; set; }
        public virtual DbSet<todayWork> todayWorks { get; set; }
        public virtual DbSet<transaction> transactions { get; set; }
        public virtual DbSet<userAuthorization> userAuthorizations { get; set; }
        public virtual DbSet<userLoginHistory> userLoginHistorys { get; set; }
        public virtual DbSet<userLogin> userLogins { get; set; }
        public virtual DbSet<userRole> userRoles { get; set; }
        public virtual DbSet<workHoliday> workHolidays { get; set; }
        public virtual DbSet<workingDayCustomer> workingDayCustomers { get; set; }
        public virtual DbSet<workingDayDenso> workingDayDensos { get; set; }
        public virtual DbSet<XlossItem> XlossItems { get; set; }
        public virtual DbSet<XlossRecord> XlossRecords { get; set; }
        public virtual DbSet<xOEE_Line> xOEE_Line { get; set; }
        public virtual DbSet<xOEE_MC> xOEE_MC { get; set; }
        public virtual DbSet<xoeeLine> xoeeLines { get; set; }
        public virtual DbSet<Prod_DensoWorkingDayTable> Prod_DensoWorkingDayTable { get; set; }
        public virtual DbSet<Prod_PPASTable> Prod_PPASTable { get; set; }
        public virtual DbSet<productionVolume> productionVolumes { get; set; }
    }
}
