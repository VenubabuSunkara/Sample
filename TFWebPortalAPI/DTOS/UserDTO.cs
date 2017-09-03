using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransForce.API.Entitys;

namespace TransForce.API.DTOS
{
    public class UserDTO
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public AspNetUser()
        //{
        //    this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
        //    this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
        //    this.AspNetRoles = new HashSet<AspNetRole>();
        //}

        public string Id { get; set; }
        public string Title { get; set; }
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public List<string> Roles { get; set; }

        //   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        //   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}