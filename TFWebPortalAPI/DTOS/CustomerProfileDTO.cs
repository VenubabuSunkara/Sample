using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TransForce.API.Entitys;

namespace TransForce.API.DTOS
{
    public class CustomerProfileDTO
    {
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "The CustomerName is Required.")]
        [MaxLength(75, ErrorMessage = "The max. length of the CustomerName is 75 characters.")]
        public string CustomerName { get; set; }

        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "The AccountOwnerName is Required.")]
        [MaxLength(50, ErrorMessage = "The max. length of the CustomerName is 50 characters.")]
        public string AccountOwnerName { get; set; }

        [Required(ErrorMessage = "The AccountOwnerEmail is Required.")]
        [MaxLength(50, ErrorMessage = "The max. length of the AccountOwnerEmail is 50 characters.")]
        [RegularExpression(@"^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$", ErrorMessage = "Not a valid Email Address.")]
        public string AccountOwnerEmail { get; set; }

        [Required(ErrorMessage = "CompanyLogo is Required.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Choose a file .JPG, .JPEG , .GIF or .PNG file")]
        public string CompanyLogo { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]
        [StringLength(50, ErrorMessage = "The max. length of the City is 50 characters.")]
        public string City { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only Alphabets allowed.")]
        [StringLength(50, ErrorMessage = "The max. length of the State is 50 characters.")]
        public string State { get; set; }

        [RegularExpression(@"", ErrorMessage = "ZipCode is not valid.")]
        [MaxLength(10, ErrorMessage = "The max. length of the ZipCode is 10 characters.")]
        [MinLength(4, ErrorMessage = "The max. length of the ZipCode is 4 characters.")]
        public string ZipCode { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> isActive { get; set; }
        public int DotCount { get; set; }
        public int UserCount { get; set; }
        //public ICollection<CustomerDOTNumberDTO> CustomerDOTNumbers { get; set; }
        //public ICollection<Location_Configuration> Location_Configuration { get; set; }
        //public ICollection<LocationMetadata> LocationMetadata { get; set; }
    }
}