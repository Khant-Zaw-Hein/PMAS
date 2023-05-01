using Newtonsoft.Json;
using PMASAPI.Base;
using PMASAPI.Models;
using PMASAPI.Validator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PMASAPI.Controllers
{
    public class LoginController : ApiController
    {
        PMASEntities _entities = new PMASEntities();
        PMAS_Backend_KeyServerEntities _KeyServerEntities = new PMAS_Backend_KeyServerEntities();

        [HttpPost]
        [Route("~/api/Login/Login")]
        public async Task<IHttpActionResult> Login(APIRequestModel request)
        {
            var responseModel = new APIResponseModel<APIPGPResponseModel>();

            _entities = new PMASEntities();
            _KeyServerEntities = new PMAS_Backend_KeyServerEntities();
            try
            {
                ApplicationLog.WriteInfo("start check handshaketoken");
                var requestcode = WebUtility.HtmlEncode(request.HandShakeToken);
                if (!(AuthenticateUtility.VerifyAuthentication(_entities, requestcode)))
                {
                    ApplicationLog.WriteError("fail to check handshaketoken");
                    responseModel.AddError("Authentication Failed.");
                    return Ok(responseModel);
                }
                ApplicationLog.WriteInfo("token check success");
                ApplicationLog.WriteInfo("request pgp encrypt data :" + request.RequestPGPEncryptString);
                var decryptString = await PGPHelper.PGP_DataDecrypt
                    (request.RequestPGPEncryptString, _entities, _KeyServerEntities);
                var requestModel = JsonConvert.DeserializeObject<LoginModel.Request>(decryptString);
                ApplicationLog.WriteInfo("pgp decrypt data :" + decryptString);


                LoginModelValidator validator = new LoginModelValidator();
                var validatorResult = validator.Validate(requestModel);

                if (!validatorResult.IsValid)
                {
                    ApplicationLog.WriteError(validatorResult.Errors[0].ErrorMessage);
                    responseModel.AddError(validatorResult.Errors[0].ErrorMessage);
                    return Ok(responseModel);
                }

                tbl_UserAccount tbl_UserAccountObj = (from c in _entities.tbl_UserAccount
                                                      where (c.LoginID.Equals(requestModel.LoginID, StringComparison.OrdinalIgnoreCase))
                                                      && c.Active.Equals(true)
                                                      select c)
                                          .FirstOrDefault<tbl_UserAccount>();


                if (tbl_UserAccountObj == null)
                {

                    responseModel.AddError("Invalid Login.");
                    return Ok(responseModel);
                }

                if (tbl_UserAccountObj.IsLocked)
                {
                    responseModel.AddError("Account is locked.Please contact to admin.");
                    return Ok(responseModel);
                }

                if (!tbl_UserAccountObj.IsEnable)
                {
                    responseModel.AddError("Account is not enabled.Please contact to admin.");
                    return Ok(responseModel);
                }

                tbl_UserProfile tbl_UserProfileObj = new tbl_UserProfile();
                tbl_UserProfileObj = _entities.tbl_UserProfile.Where(x => x.UserAccountID == tbl_UserAccountObj.ID).FirstOrDefault();

                var _encryptPassword = CryptoSecurity.HashCode(CryptoSecurity.GetBytes(requestModel.Password), tbl_UserAccountObj.Saltkey);
                if (tbl_UserAccountObj.Password.Equals(_encryptPassword, StringComparison.OrdinalIgnoreCase))
                {

                    var settingdata = _entities.tbl_CodeTable.Where(x => x.Type == "AppSetting" && x.Name == "PasswordValidity" && x.Active == true).FirstOrDefault();

                    DateTime compareDate = DateTime.Now.AddDays(-(double)settingdata.Value);
                    if (tbl_UserAccountObj.LastPasswordChangeDate <= compareDate)
                    {
                        responseModel.AddError("Need password change");
                        //return Ok(responseModel);
                    }

                    var data = (from tblUserAccount in _entities.tbl_UserAccount
                                join tblUserProfile in _entities.tbl_UserProfile
                                on tblUserAccount.ID equals tblUserProfile.UserAccountID
                                //join tblUserRole in _entities.tbl_UserRole
                                //on tblUserAccount.UserRoleID equals tblUserRole.ID
                                where tblUserAccount.Active == true
                                && tblUserAccount.LoginID == requestModel.LoginID
                                select new LoginModel.Response
                                {
                                    UserAccountID = tblUserAccount.ID,
                                    LoginID = tblUserAccount.LoginID,
                                    Name = tblUserProfile.Name,
                                    Email = tblUserProfile.email,
                                    //UserRoleName = tblUserRole.Name,
                                    CreatedDateTime = tblUserAccount.CreatedDate,
                                    Status = tblUserAccount.Active,
                                    //UserRoleID = tblUserRole.ID
                                }).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();

                    tbl_UserAccountObj.LoginFailedAttempt = 0;
                    tbl_UserAccountObj.IsFirstTimeLogin = false;
                    tbl_UserAccountObj.LastLoginTime = DateTime.Now;
                    _entities.Entry(tbl_UserAccountObj).State = EntityState.Modified;
                    _entities.SaveChanges();

                    string ret = await PGPHelper.PGP_DataEncrypt(JsonConvert.SerializeObject(data), _entities, _KeyServerEntities);
                    responseModel.Response = new APIPGPResponseModel
                    {
                        ResponsePGPEncryptString = ret,
                    };
                }
                else
                {
                    tbl_UserAccountObj.LoginFailedAttempt = tbl_UserAccountObj.LoginFailedAttempt + 1;
                    var settingdata = _entities.tbl_CodeTable.Where(x => x.Type == "AppSetting" && x.Name == "LoginFailedAttempt" && x.Active == true).FirstOrDefault();

                    if (tbl_UserAccountObj.LoginFailedAttempt >= settingdata.Value)
                    {
                        tbl_UserAccountObj.IsLocked = true;
                    }
                    _entities.Entry(tbl_UserAccountObj).State = EntityState.Modified;
                    _entities.SaveChanges();
                    responseModel.AddError("Password is incorrect.");
                    return Ok(responseModel);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteError(ex.Message.ToString());
                responseModel.AddError(ex.Message.ToString());
                return Ok(responseModel);
            }
            ApplicationLog.WriteInfo("Api Response :" + responseModel);
            return Ok(responseModel);
        }
    }
}
