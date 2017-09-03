using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransForce.Web.Portal.Models
{
    public class ManualRSI
    {
        public int ItemId { get; set; }
        public int CompanyID { get; set; }
        public string DOTNumber { get; set; }
        public int InspectionID { get; set; }
        public int ReportNumber { get; set; }
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

        public virtual CustomerProfile CustomerProfile { get; set; }
    }
}