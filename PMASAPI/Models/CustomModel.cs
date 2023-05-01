using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Models
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
        //public Guid ChildProgramID { get; set; }
        //public string ChildProgramName { get; set; }
        //public string ChildProgramURL { get; set; }
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
    public class MenuLevelModel
    {
        public int Level { get; set; }
    }
}