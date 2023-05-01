using PMASAPI.Base;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace PMASAPI.Controllers
{
    public class AuthenticateController : ApiController
    {
        // GET: Authenticate
        [HttpPost]
        [Route("~/api/Authenticate/GetRequestAuthCode")]
        public IHttpActionResult GetRequestAuthCode(RequestPublicKeyModel requestModel)
        {
            PMASEntities _entities = new PMASEntities();
            ApplicationLog.WriteInfo("enter get request handshaketoken key");
            string requestcode = WebUtility.HtmlEncode(requestModel.APIHandshakeKey);
            IList<tbl_WebAPISetting> tblAPIConfig = _entities.tbl_WebAPISetting.Where(x => x.PublicKey == requestcode && x.Active == true).ToList();

            if (tblAPIConfig.Count == 0)
            {
                ApplicationLog.WriteError("This request key :" + requestModel.APIHandshakeKey + " is not found");
                return NotFound();
            }
            string generateString = GetRandomString(8);
            string privateKey = tblAPIConfig[0].PrivateKey;
            string encryptedString = getHashData(generateString + privateKey);

            tbl_WebAPI_RequestLog tblRequestLog = new tbl_WebAPI_RequestLog();
            tblRequestLog.ID = Guid.NewGuid();
            tblRequestLog.RequestedSouceID = tblAPIConfig[0].ID;
            tblRequestLog.RequestedPublicKey = tblAPIConfig[0].PublicKey;
            tblRequestLog.GeneratedKeyword = generateString;
            tblRequestLog.EncryptedString = encryptedString;
            tblRequestLog.Status = "NEW";
            tblRequestLog.CreatedDate = DateTime.Now;

            _entities.tbl_WebAPI_RequestLog.Add(tblRequestLog);
            _entities.SaveChanges();

            return Ok(generateString);
        }

        #region Custom Method
        protected string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        protected string getHashData(string reqeustHushString)
        {
            System.Security.Cryptography.SHA512 sha512 = new System.Security.Cryptography.SHA512Managed();

            byte[] sha512Bytes = System.Text.Encoding.Default.GetBytes(reqeustHushString);

            byte[] cryString = sha512.ComputeHash(sha512Bytes);

            string hashedPwd = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                hashedPwd += cryString[i].ToString("X2");
            }

            return hashedPwd;
        }
        #endregion
    }
}
