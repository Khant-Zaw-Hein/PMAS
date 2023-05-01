using Org.BouncyCastle.Asn1.Crmf;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using RestSharp;

namespace PMASAPI.Base
{
    public class AuthenticateUtility
    {
        public static bool VerifyAuthentication(PMASEntities _context, string userRequestCode)
        {
            bool returnValue = false;

            userRequestCode = WebUtility.HtmlEncode(userRequestCode);
            IList<tbl_WebAPI_RequestLog> tblRequestList = _context.tbl_WebAPI_RequestLog
                                                                  .Where(x => x.EncryptedString == userRequestCode &&
                                                                            x.Status == "NEW")
                                                                  .ToList();

            if (tblRequestList.Count == 0)
            {
                returnValue = false;
                return returnValue;
            }

            //DateTime compareTime = tblRequestList[0].CreatedDate.AddMinutes(2);
            DateTime compareTime = tblRequestList[0].CreatedDate.AddMinutes(10);
            int result = DateTime.Compare(DateTime.Now, compareTime);

            //later than
            if (result > 0)
            {
                returnValue = false;
                return returnValue;
            }


            Guid updateRecordID = tblRequestList[0].ID;

            tbl_WebAPI_RequestLog tblRequestLog = _context.tbl_WebAPI_RequestLog
                                                        .Where(x => x.ID == updateRecordID)
                                                        .Single();
            tblRequestLog.Status = "USED";
            tblRequestLog.UpdatedDate = DateTime.Now;

            _context.Entry(tblRequestLog).State = EntityState.Modified;
            _context.SaveChanges();

            returnValue = true;

            return returnValue;
        }

        public static string requestAuthCode(string Base_URL, String CloudAPIHandshakeKey)
        {
            string handShakeKey = String.Empty;
            RequestPublicKeyModel model = new RequestPublicKeyModel();
            model.APIHandshakeKey = CloudAPIHandshakeKey;

            var webUrl = Base_URL + @"Authenticate/GetRequestAuthCode";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(webUrl);
            client.Timeout = 50000; // 5000 milliseconds == 5 seconds

            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(model);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    ApplicationLog.WriteError(System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + response.ErrorMessage);
                }
            }
            else
            {
                handShakeKey = response.Content.Replace("\"", string.Empty).Trim();
            }
            return handShakeKey;
        }
    }
}
