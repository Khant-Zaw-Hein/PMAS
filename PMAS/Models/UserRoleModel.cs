using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class RolePermissionViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<MenuViewModel> MenuViewModel { get; set; }
        public Guid CurrentUserID { get; set; }
    }
       public class UserRoleCreateModel
    {
        public int ID { get; set; }
        public Guid UserAccountId { get; set; }
        public Guid RoleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

    }
    public class UserRoleViewModel
    {
        public int ID { get; set; }
        public Guid UserAccountId { get; set; }
        public Guid RoleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}