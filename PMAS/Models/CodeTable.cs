using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class SearchCodeTableViewModel
    {
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public string Name { get; set; }
        public List<Sort> SortList { get; set; }
    }

    public class CodeTableViewModel
    {
        public Guid ID { get; set; }
        public string CodeType { get; set; }
        public string CodeName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CodeTableCreateModel
    {
        public Guid ID { get; set; }
        public string CodeType { get; set; }
        public string CodeName { get; set; }
        public Guid CurrentUserID { get; set; }
    }

    public class tbl_CodeTable
    {
        public System.Guid ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
        public bool Active { get; set; }
    }
}