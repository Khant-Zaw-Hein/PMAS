//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMASAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_PerformanceRating
    {
        public int PerformanceRating_Id { get; set; }
        public string performance_rating { get; set; }
        public string perf_cat { get; set; }
        public Nullable<bool> disabled { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
    }
}
