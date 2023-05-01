using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace PMASAPI.Models
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
    }
    public class ProgramCreateModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ProgramURL { get; set; }
        public bool IsParentProgram { get; set; }
        public Guid ParentProgramID { get; set; }
        public Guid CurrentUserID { get; set; }
    }
}