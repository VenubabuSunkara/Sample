using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TransForce.API.Entitys;

namespace TransForce.API.DTOS
{
    public class CustomerDOTNumberDTO
    {
        public int ItemID { get; set; }

        [Required(ErrorMessage = "The CustomerID is required.")]
        public int CustomerID { get; set; }
        [Required]
        [MaxLength(128, ErrorMessage = "The max. length of the DOTNumber is 128 characters.")]
        public string DOTNumber { get; set; }
        [MaxLength(500, ErrorMessage = "The max. length of the DOTTag is 500 characters.")]
        public string DOTTag { get; set; }
        [Required(ErrorMessage = "The IsActive accept only true or false.")]
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

    //    public CustomerProfile CustomerProfile { get; set; }
    }
}