//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransForce.API.Entitys
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocationFieldType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocationFieldType()
        {
            this.LocationMetadatas = new HashSet<LocationMetadata>();
        }
    
        public int LocFID { get; set; }
        public string FieldType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocationMetadata> LocationMetadatas { get; set; }
    }
}