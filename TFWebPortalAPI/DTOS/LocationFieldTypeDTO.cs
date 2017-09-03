using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransForce.API.DTOS
{
    public class LocationFieldTypeDTO
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LocationFieldType()
        //{
        //    this.LocationMetadata = new HashSet<LocationMetadata>();
        //}
        public int LocFID { get; set; }
        public string FieldType { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<LocationMetadata> LocationMetadata { get; set; }
    }
}