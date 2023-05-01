using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMASAPI.Models
{
    public class UserAccountViewModel
    {
        public Guid UserAccountID { get; set; }
        public string LoginID { get; set; }
        //public Guid UserRoleID { get; set; }
        public String UserRoleName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Department_Id { get; set; }
        public int Designation_Id { get; set; }
        public int Rank_Id { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Rank { get; set; }
        public List<Guid> Role_Ids { get; set; }
        public Guid RO_Id { get; set; }
        public string JobDescription { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? LastDayDate { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEnable { get; set; }
        public bool Active { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public string CreatedDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
    public class UserAccountCreateModel
    {
        public Guid UserAccountID { get; set; }
        public string LoginID { get; set; }
        //public Guid UserRoleID { get; set; }
        public string Password { get; set; }
        public bool IsEnable { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Department_Id { get; set; }
        public int Designation_Id { get; set; }
        public int Rank_Id { get; set; }
        public List<Guid> Role_Ids { get; set; }
        public Guid RO_Id { get; set; }
        public string JobDescription { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? LastDayDate { get; set; }
        public Guid CurrentUserID { get; set; }
    }

    public class UserAccountInfoModel
    {
        public Guid ID { get; set; }
        public string LoginID { get; set; }
        public byte[] Saltkey { get; set; }
        public string DecryptPassword { get; set; }
        public bool isAuthenticate { get; set; }
    }
}