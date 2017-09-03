using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransForce.API.DTOS
{
    public class LocationDTO
    {
        public int LocId { get; set; }
        public string LocName { get; set; }
        public int CompanyID { get; set; }
        //  public virtual CustomerProfile CustomerProfile { get; set; }
        //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        // public virtual ICollection<LocationMetadata> LocationMetadata { get; set; }
    }
}