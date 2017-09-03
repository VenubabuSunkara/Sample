using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransForce.Web.Portal.Models
{
    public class Inspection
    {
        public int CompanyId { get; set; }
        public RPT_REG_CARRIER_PROFILE_XML xml { get; set; }
    }
 public class BlobUploadModel
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public byte[] FileData { get; set; }
        public int InspectionId { get; set; }
        public int CompanyId { get; set; }
    }
    public class InspectonViewModel
    {
        public CustomerProfile Customer { get; set; }
        public SelectList Locations { get; set; }
    }
    public class LocationList
    {
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
    }

    public class InspectionDriver
    {
        public int CompanyId { get; set; }
        public DateTime InspectionDate { get; set; }
        public int InspectionId { get; set; }
        public string DriverName { get; set; }
    }

    public class ManualRSIModel
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
        public string InspectionDocuments { get; set; }
        public Nullable<int> INSP_ID { get; set; }

        public virtual CustomerProfile CustomerProfile { get; set; }
    }
}