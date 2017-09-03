using System;
using System.Collections.Generic;
using TransForce.API.Entitys;

namespace TransForce.API.Models
{
    public class CustomerModels
    {
        public CustomerModels()
        {
            this.Location_Configuration = new HashSet<Location_Configuration>();
        }

        public int CompanyID { get; set; }
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountOwnerName { get; set; }
        public string AccountOwnerEmail { get; set; }
        public byte[] CompanyLogo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public Nullable<bool> isActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location_Configuration> Location_Configuration { get; set; }
    }
    public class EditCustomer
    {
        public int CompanyID { get; set; }
    }

}