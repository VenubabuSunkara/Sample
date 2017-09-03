using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TransForce.Web.Portal.Models
{
    public class CustomerViewModel
    {
        [Required]
        [Display(Name = "Company Name")]
        public string CustomerName { get; set; }
        
        [Display(Name = "Account Name")]
        public string AccountNumber { get; set; }
        [Required]
        [Display(Name = "Account Owner Name")]
        public string AccountOwnerName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        [Display(Name = "Account Owner Email")]
        public string AccountOwnerEmail { get; set; }

        public String CompanyLogo { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

    }
}