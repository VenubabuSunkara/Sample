using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransForce.Web.Portal.Models
{
    public class LocationConfigurationViewModel
    {
        public int LocConfigID { get; set; }

        [Required]
        public Nullable<int> CompanyID { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public Nullable<int> Relation { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual CustomerProfile CustomerProfile { get; set; }
    }
}