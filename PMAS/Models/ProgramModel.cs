using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class SearchProgramTableViewModel
    {
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public string Name { get; set; }
        public List<Sort> SortList { get; set; }
    }
    public class ProgramViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ProgramURL { get; set; }
        public bool IsParentProgram { get; set; }
        public Guid? ParentProgramID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class ProgramCreateModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ProgramURL { get; set; }
        public bool IsParentProgram { get; set; }
        public Guid? ParentProgramID { get; set; }
        public Guid CurrentUserID { get; set; }
    }
    public class tbl_Program
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string ProgramURL { get; set; }
        public bool IsParentProgram { get; set; }
        public Nullable<System.Guid> ParentProgramID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public bool Active { get; set; }
    }
}