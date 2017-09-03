using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TransForce.API.Entitys;

namespace TransForce.API.DTOS
{
    public class LocationConfigurationDTO
    {
        public int LocConfigID { get; set; }

        [Required(ErrorMessage = "The CompanyID is required.")]
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "The Key is required.")]
        [MaxLength(50,ErrorMessage = "The max. length of the Key is 50 characters.")]
        public string Key { get; set; }

        [Required(ErrorMessage = "The Value is required.")]
        [MaxLength(50, ErrorMessage = "The max. length of the Value is 50 characters.")]
        public string Value { get; set; }

        [Required(ErrorMessage = "The Relation is required.")]
        public int Relation { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

     //   public  CustomerProfile CustomerProfile { get; set; }
     //   public  ICollection<LocationMetadata> LocationMetadata { get; set; }
    }
}