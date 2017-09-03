using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransForce.Web.Portal.Models
{
    public class G_RPT_REG_CP_INSP_IDModel
    {
        public int ID { get; set; }
        public string TransID { get; set; }
        public string DOTNumber { get; set; }
        public Nullable<int> INSPECTION_ID { get; set; }
        public string UNIT_NUMBER1 { get; set; }
        public string UNIT_TYPE1 { get; set; }
        public string VEH_ST_LICENSE1 { get; set; }
        public string UNIT_VIN1 { get; set; }
        public string COMPANY_NUMBER1 { get; set; }
        public string UNIT_NUMBER2 { get; set; }
        public string UNIT_TYPE2 { get; set; }
        public string VEH_ST_LICENSE2 { get; set; }
        public string UNIT_VIN2 { get; set; }
        public string COMPANY_NUMBER2 { get; set; }
        public string UNIT_NUMBER3 { get; set; }
        public string UNIT_TYPE3 { get; set; }
        public string VEN_ST_LICENSE3 { get; set; }
        public string UNIT_VIN3 { get; set; }
        public string COMPANY_NUMBER3 { get; set; }
        public string UNIT_NUMBER4 { get; set; }
        public string UNIT_TYPE4 { get; set; }
        public string VEN_ST_LICENSE4 { get; set; }
        public string UNIT_VIN4 { get; set; }
        public string COMPANY_NUMBER4 { get; set; }
        public string UNIT_NUMBER5 { get; set; }
        public string UNIT_TYPE5 { get; set; }
        public string VEH_ST_LICENSE5 { get; set; }
        public string UNIT_VIN5 { get; set; }
        public string COMPANY_NUMBER5 { get; set; }
        public string UNIT_NUMBER6 { get; set; }
        public string UNIT_TYPE6 { get; set; }
        public string VEH_ST_LICENSE6 { get; set; }
        public string UNIT_VIN6 { get; set; }
        public string COMPANY_NUMBER6 { get; set; }
        public string DRIVER_LAST_NAME1 { get; set; }
        public string DRIVER_FIRST_NAME1 { get; set; }
        public string DRIVER_MI1 { get; set; }
        public string INSP_LEVEL_CODE { get; set; }
        public Nullable<System.DateTime> INSP_DATE { get; set; }
        public string INSP_TIME { get; set; }
        public string REPORT_STATE { get; set; }
        public string REPORT_NUMBER1 { get; set; }
        public string COUNTY_NAME1 { get; set; }
        public string LOCATION1 { get; set; }
        public string HM { get; set; }
        public Nullable<int> TOTAL_VIOL { get; set; }
        public Nullable<int> TOTAL_OOS { get; set; }
        public string CARRIER_NAME1 { get; set; }
        public string CARRIER_CITY1 { get; set; }
        public string CARRIER_STATE1 { get; set; }
        public string DRIVER_LICENSE { get; set; }
        public string DRIVER_LICENSE_STATE { get; set; }
        public Nullable<System.DateTime> DRIVER_DOB { get; set; }
        public string ImportMessage { get; set; }
        public Nullable<int> DriverId { get; set; }
        public string LocationCode { get; set; }
        public string Comment { get; set; }
        public string DriverLogProvided { get; set; }
        public string VehicleRepairOrderProvided { get; set; }
        public string DriverDisciplineIssued { get; set; }
        public string DriverRelatedViolation { get; set; }
        public string FailedToReportInspection { get; set; }
        public string FailPreTripInspection { get; set; }
        public string ImproperLogFile { get; set; }
        public string DocsRequested { get; set; }
        public Nullable<int> DocsRequestedCount { get; set; }
        public Nullable<System.DateTime> InitDocRequestDate { get; set; }
        public Nullable<System.DateTime> LastDocRequestDate { get; set; }
        public string DeniedInspection { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string InspectionLinked { get; set; }
        public string ReportNbrKey { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> CompanyId { get; set; }
    }
}