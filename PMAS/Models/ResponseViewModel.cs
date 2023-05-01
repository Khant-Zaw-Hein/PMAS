using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAS.Models
{
    public class ResponseViewModel
    {
        public string verifyLink { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public static class Messagetype
    {
        public static string SaveSuccess
        {
            get
            {
                return "Save Success!";
            }
        }

        public static string UpdateSuccess
        {
            get
            {
                return "Update Success!";
            }
        }

        public static string DeleteSuccess
        {
            get
            {
                return "Delete Success!";
            }
        }

        public static string Failed
        {
            get
            {
                return "Process failed. Please contact to Seacare IT support team. Error message : ";
            }
        }

        public static string CanNotConnectAPI
        {
            get
            {
                return "Cannot connect to api";
            }
        }
    }

    public static class MessageStatus
    {
        public static string Success
        {
            get
            {
                return "Success";
            }
        }

        public static string Failed
        {
            get
            {
                return "Fail";
            }
        }
    }
}