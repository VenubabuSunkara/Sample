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
    using System.Web.Mvc;

    public partial class CustomerProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerProfile()
        {
            this.CustomerDOTNumbers = new HashSet<CustomerDOTNumber>();
            this.Location_Configuration = new HashSet<Location_Configuration>();
            this.LocationMetadata = new HashSet<LocationMetadata>();
        }

        public int CompanyID { get; set; }
        [Display(Name = "Company Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Account Owner Name")]
        public string AccountOwnerName { get; set; }
        [Display(Name = "Account Owner Email")]
        public string AccountOwnerEmail { get; set; }
        [Display(Name = "Company Logo")]
        public string CompanyLogo { get; set; }
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        [Display(Name = "Status")]
        public Nullable<bool> isActive { get; set; }

        public SelectList CompanyList { get; set; }
        public string StatusMessage { get; set; }
        public int DotCount { get; set; }

        public string SuccessMesage { get; set; }
        public string UploadFailMessage { get; set; }
        public string ExMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDOTNumber> CustomerDOTNumbers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location_Configuration> Location_Configuration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocationMetadata> LocationMetadata { get; set; }
    }

    public class StatusResult
    {
        public string Status { get; set; }
        public object Result { get; set; }
    }
}