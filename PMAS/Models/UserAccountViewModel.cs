using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
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
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int Department_Id { get; set; }
        public int Designation_Id { get; set; }
        public int Rank_Id { get; set; }
        public Guid RO_Id { get; set; }
        public List<Guid> Role_Ids { get; set; }
        public string JobDescription { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? LastDayDate { get; set; }
        public Guid CurrentUserID { get; set; }
    }
    public class tbl_UserAccount
    {
        public System.Guid ID { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        //public System.Guid UserRoleID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public bool Active { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEnable { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
        public Nullable<System.DateTime> LastPasswordChangeDate { get; set; }
        public Nullable<bool> IsFirstTimeLogin { get; set; }
        public byte[] Saltkey { get; set; }
    }

    public class PasswordChangeModel
    {
        public Guid LoginID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordChangeType { get; set; }
    }
    public partial class tbl_Department
    {
        public int Department_Id { get; set; }
        public string department { get; set; }
        public Nullable<bool> disabled { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
    }
    public partial class tbl_Designation
    {
        public int Designation_Id { get; set; }
        public string designation { get; set; }
        public Nullable<bool> disabled { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
        public Nullable<int> sort_order { get; set; }
    }
    public class tbl_Rank
    {
        public int Rank_Id { get; set; }
        public string rank { get; set; }
        public Nullable<bool> disabled { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.DateTime> modified_date { get; set; }
        public Nullable<int> modified_by { get; set; }
    }
    public partial class tbl_Role
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> SortIndex { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public bool Active { get; set; }
    }

}