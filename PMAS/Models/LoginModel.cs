using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class LoginModel
    {
        public class Request
        {
            public string LoginID { get; set; }
            public string Password { get; set; }
            public string HandShakeToken { get; set; }
        }
        public class Response
        {
            public Guid UserAccountID { get; set; }
            public string LoginID { get; set; }
            //public Guid UserRoleID { get; set; }
            public string UserRoleName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string CreatedDate { get; set; }
            public DateTime CreatedDateTime { get; set; }
            public bool Status { get; set; }
        }
    }
}