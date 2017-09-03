using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TransForce.API.Entitys;

namespace TransForce.API.DTOS
{
    public class LocationMetadataDTO
    {
        public int LocMID { get; set; }
        public int CustomerID { get; set; }
        public int LocConfigID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int FieldType { get; set; }
        public string Tags { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public int LocId { get; set; }

        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual Location Location { get; set; }
        public virtual Location_Configuration Location_Configuration { get; set; }
        public virtual LocationFieldType LocationFieldType { get; set; }
    }
}