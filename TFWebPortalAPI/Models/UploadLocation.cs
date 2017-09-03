using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransForce.API.Models
{
    public class UploadLocation
    {
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public List<LocationData> Locationdata { get; set; }
    }
    public class LocationValidationData
    {
        public UploadLocation ValidData { get; set; }
        public UploadLocation UnValidData { get; set; }
    }
    public class LocationData
    {
        // [Required]
        public string LocationName { get; set; }
        // [Required]        
        public string LocCode { get; set; }
        //   [Required]
        public string FirstContactName { get; set; }
        //   [Required]
        //  [EmailAddress]
        public string FirstContactEmail { get; set; }
        public string isActive { get; set; }
        public string SecondContactName { get; set; }
        //   [EmailAddress]
        public string SecondContactEmail { get; set; }
        public string ThirdContactName { get; set; }
        //  [EmailAddress]
        public string ThirdContactEmail { get; set; }
        public string Tags { get; set; }
        public string Remarks { get; set; }
    }
}