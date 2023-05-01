using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class Menu_ProgramViewModel
    {
        public Guid MenuID { get; set; }
        public string MenuName { get; set; }
        public Guid ParentMenuID { get; set; }
        public int MenuLevel { get; set; }
        public string MenuCategory { get; set; }
        public int? SerialIndexNo { get; set; }
        public Guid programID { get; set; }
        public string ProgramURL { get; set; }
        public Guid ChildProgramID { get; set; }
        public string ChildProgramName { get; set; }
        public string ChildProgramURL { get; set; }
    }
    public class MenuViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ProgramName { get; set; }
        public Guid ProgramID { get; set; }
        public int MenuLevel { get; set; }
        public string ParentMenuName { get; set; }
        public Guid ParentMenuID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsShow { get; set; }
        public Guid ChildProgramID { get; set; }
        public string ChildProgramName { get; set; }
    }

    public class MenuCreateModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ProgramName { get; set; }
        public Guid ProgramID { get; set; }
        public int MenuLevel { get; set; }
        public string ParentMenuName { get; set; }
        public Guid ParentMenuID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsShow { get; set; }
        public Guid ChildProgramID { get; set; }
        public string ChildProgramName { get; set; }
        public Guid CurrentUserID { get; set; }
    }
    public class tbl_Menu
    {
        public System.Guid ID { get; set; }
        public string MenuName { get; set; }
        public Nullable<System.Guid> ProgramID { get; set; }
        public int MenuLevel { get; set; }
        public Nullable<System.Guid> ParentMenuID { get; set; }
        public Nullable<int> SortIndexSerialNo { get; set; }
        public bool IsShow { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public bool Active { get; set; }
    }
    public class MenuLevelModel
    {
        public int Level { get; set; }
    }
}