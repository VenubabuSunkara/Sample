//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransForce.API.Entitys
{
    using System;
    using System.Collections.Generic;
    
    public partial class ManualRSI
    {
        public int ItemId { get; set; }
        public int CompanyID { get; set; }
        public string DOTNumber { get; set; }
        public int InspectionID { get; set; }
        public string ReportNumber { get; set; }
        public string DriverLastName { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string DriverLicenseState { get; set; }
        public System.DateTime InspectionDate { get; set; }
        public string InspectionState { get; set; }
        public string Violations { get; set; }
        public string OOS { get; set; }
        public string NoticeNumber { get; set; }
        public Nullable<bool> DisciplineActionIssued { get; set; }
        public Nullable<bool> RepairWorkOrder { get; set; }
        public Nullable<bool> DriverLog { get; set; }
        public Nullable<bool> InspectionReceived { get; set; }
        public string VehicleLicenseNumber { get; set; }
        public string CompanyVehicleNumber { get; set; }
        public Nullable<bool> OtherViolation { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string LocationCode { get; set; }
        public Nullable<System.DateTime> DriverDOB { get; set; }
        public string AdminNotes { get; set; }
        public Nullable<int> INSP_ID { get; set; }
    
        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual G_RPT_REG_CP_INSP_ID G_RPT_REG_CP_INSP_ID { get; set; }
    }
}
