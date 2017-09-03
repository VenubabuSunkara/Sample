using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransForce.Web.Models
{
    public class DriversModel
    {
        public string DotNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LicenseState { get; set; }
        public string LicenseNumber { get; set; }
        public string DOB { get; set; }
        public string isActive { get; set; }
        public string Remarks { get; set; }
    }
    public class UploadDrivers
    {
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public List<DriversModel> Driverdata { get; set; }
        public List<List<DriversModel>> ChunkDriverdata { get; set; }
    }
}