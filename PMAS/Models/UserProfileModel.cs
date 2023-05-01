﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class UserProfileModel
    {
    }
    public partial class tbl_UserProfile
    {
        public System.Guid ID { get; set; }
        public System.Guid UserAccountID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public Nullable<int> Department_Id { get; set; }
        public Nullable<int> Designation_Id { get; set; }
        public Nullable<int> Rank_Id { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public Nullable<System.DateTime> ConfirmDate { get; set; }
        public Nullable<System.DateTime> LastDayDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> Deleted { get; set; }
        public Nullable<System.Guid> DeletedBy { get; set; }
        public bool Active { get; set; }
        public string JobDescriptionForm { get; set; }
        public Nullable<System.Guid> ReportingOfficer { get; set; }
    }
}