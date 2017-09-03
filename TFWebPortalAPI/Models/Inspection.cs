using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TransForce.API.Models
{
    public class BlobUploadModel
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public byte[] FileData { get; set; }
        public int InspectionId { get; set; }
        public int CompanyId { get; set; }
    }
    public class Inspection
    {
        public int CompanyId { get; set; }
        public RPT_REG_CARRIER_PROFILE_XML xml { get; set; }
    }
    public class InspectionDriver
    {
        public int CompanyId { get; set; }
        public DateTime InspectionDate { get; set; }
        public int InspectionId { get; set; }
        public string DriverLicence { get; set; }

    }
    public class InspectionDetailModel
    {
        public string DOTNumber { get; set; }
        public Nullable<int> INSPECTION_ID { get; set; }
        public string REPORT_NUMBER1 { get; set; }
        public string REPORT_STATE { get; set; }
        public string DRIVER_LICENSE { get; set; }
        public string DRIVER_LICENSE_STATE { get; set; }
        public Nullable<System.DateTime> DRIVER_DOB { get; set; }
        public string DRIVER_LAST_NAME1 { get; set; }
        public string DRIVER_FIRST_NAME1 { get; set; }
        public string DRIVER_MI1 { get; set; }
        public string LOCATION1 { get; set; }


    }
}