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
    
    public partial class Inspection_Documents
    {
        public int DocumentId { get; set; }
        public string DocumentUrl { get; set; }
        public int ManualRsiId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> InspectionId { get; set; }
    
        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual Inspection_Documents Inspection_Documents1 { get; set; }
        public virtual Inspection_Documents Inspection_Documents2 { get; set; }
    }
}