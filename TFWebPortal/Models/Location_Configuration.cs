//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransForce.Web.Portal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Location_Configuration
    {
        public int LocConfigID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Nullable<int> Relation { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual CustomerProfile CustomerProfile { get; set; }
    }

    public class UploadLocation
    {
        [Required]
        public int CompanyID { get; set; }
        [Required]
        public List<LocationData> Locationdata { get; set; }
        public List<List<LocationData>> ChunkLocationdata { get; set; }
        public IProgress<int> progress { get; set; }
    }
    public class LocationData
    {
        public string LocationName { get; set; }
        public string LocCode { get; set; }
        public string isActive { get; set; }
        public string FirstContactName { get; set; }
        public string FirstContactEmail { get; set; }
        public string SecondContactName { get; set; }
        public string SecondContactEmail { get; set; }
        public string ThirdContactName { get; set; }
        public string ThirdContactEmail { get; set; }
        public string Tags { get; set; }
        public string Remarks { get; set; }
    }
    public class ValidationFile
    {
        public List<List<LocationData>> ChunkvalidData { get; set; }
        public List<LocationData> validData { get; set; }
        public List<LocationData> NotvalidData { get; set; }
    }
}
