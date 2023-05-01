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
using System.Web.UI.WebControls;

namespace PMASAPI.Controllers
{
    public class UserAccountController : ApiController
    {
        PMASEntities _entities = new PMASEntities();
        PMAS_Backend_KeyServerEntities _KeyServerEntities = new PMAS_Backend_KeyServerEntities();

        //Employee Listing
        [HttpPost]
        [Route("~/api/UserAccount/GetUserAccountData")]
        public async Task<IHttpActionResult> GetUserAccountData(APIRequestModel request)
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
                IList<UserAccountViewModel> UserAccountViewModelList = (from tblUserAccount in _entities.tbl_UserAccount
                                                                        join tblUserProfile in _entities.tbl_UserProfile
                                                                        on tblUserAccount.ID equals tblUserProfile.UserAccountID
                                                                        //join tblUserRole in _entities.tbl_UserRole
                                                                        //on tblUserAccount.UserRoleID equals tblUserRole.ID
                                                                        join tblDepartment in _entities.tbl_Department
                                                                        on tblUserProfile.Department_Id equals tblDepartment.Department_Id
                                                                        join tblDesignation in _entities.tbl_Designation
                                                                        on tblUserProfile.Designation_Id equals tblDesignation.Designation_Id
                                                                        join tblRank in _entities.tbl_Rank
                                                                        on tblUserProfile.Rank_Id equals tblRank.Rank_Id
                                                                        where tblUserAccount.Active == true
                                                                        select new UserAccountViewModel
                                                                        {
                                                                            UserAccountID = tblUserAccount.ID,
                                                                            LoginID = tblUserAccount.LoginID,
                                                                            Name = tblUserProfile.Name,
                                                                            Email = tblUserProfile.email,
                                                                            Department = tblDepartment.department,
                                                                            Designation = tblDesignation.designation,
                                                                            Rank = tblRank.rank,
                                                                            JobDescription = tblUserProfile.JobDescriptionForm,
                                                                            IsLocked = tblUserAccount.IsLocked,
                                                                            IsEnable = tblUserAccount.IsEnable,
                                                                            Active = tblUserAccount.Active,
                                                                            LastLoginTime = tblUserAccount.LastLoginTime,
                                                                            LastPasswordChangeDate = tblUserAccount.LastPasswordChangeDate,
                                                                            CreatedDateTime = tblUserAccount.CreatedDate
                                                                        }).OrderByDescending(x => x.CreatedDateTime).ToList();

                string ret = await PGPHelper.PGP_DataEncrypt(JsonConvert.SerializeObject(UserAccountViewModelList), _entities, _KeyServerEntities);
                responseModel.Response = new APIPGPResponseModel
                {
                    ResponsePGPEncryptString = ret,
                };
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

        //Get Employee Account by Id
        [HttpPost]
        [Route("~/api/UserAccount/GetUserAccountById")]
        public async Task<IHttpActionResult> GetUserAccountById(APIRequestModel request)
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
                var requestModel = JsonConvert.DeserializeObject<IDModel>(decryptString);
                ApplicationLog.WriteInfo("pgp decrypt data :" + decryptString);

                var data = (from user in _entities.tbl_UserAccount
                            join profile in _entities.tbl_UserProfile on user.ID equals profile.UserAccountID
                            where user.ID == requestModel.ID
                            select new UserAccountViewModel
                            {
                                UserAccountID = user.ID,
                                LoginID = user.LoginID,
                                Password = user.Password,
                                Name = profile.Name,
                                Email = profile.email,
                                //UserRoleID = user.UserRoleID,
                            }).FirstOrDefault();

                string ret = await PGPHelper.PGP_DataEncrypt(JsonConvert.SerializeObject(data), _entities, _KeyServerEntities);
                responseModel.Response = new APIPGPResponseModel
                {
                    ResponsePGPEncryptString = ret,

                };
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

        [HttpPost]
        [Route("~/api/UserAccount/UserAccountEdit")]
        public async Task<IHttpActionResult> UserAccountEdit(APIRequestModel request)
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
                var requestModel = JsonConvert.DeserializeObject<UserAccountCreateModel>(decryptString);
                ApplicationLog.WriteInfo("pgp decrypt data :" + decryptString);

                UserAccountUpdateModelValidator validator = new UserAccountUpdateModelValidator();
                var validatorResult = validator.Validate(requestModel);

                if (!validatorResult.IsValid)
                {
                    ApplicationLog.WriteError(validatorResult.Errors[0].ErrorMessage);
                    responseModel.AddError(validatorResult.Errors[0].ErrorMessage);
                    return Ok(responseModel);
                }

                tbl_UserAccount tblUserAccount = _entities.tbl_UserAccount
                                                          .Where(x => x.ID == requestModel.UserAccountID)
                                                          .Single();

                tbl_UserProfile tblUserProfile = _entities.tbl_UserProfile
                                                          .Where(x => x.UserAccountID == requestModel.UserAccountID)
                                                          .Single();

                string _NewEncryptPassword = string.Empty;
                _NewEncryptPassword = Base.CryptoSecurity.HashCode(Base.CryptoSecurity.GetBytes(requestModel.Password),
                                                                   tblUserAccount.Saltkey);

                tblUserAccount.LoginID = requestModel.LoginID;
                tblUserAccount.Password = _NewEncryptPassword;
                tblUserAccount.IsEnable = requestModel.IsEnable;
                //tblUserAccount.UserRoleID = requestModel.UserRoleID;
                tblUserAccount.ModifiedBy = requestModel.CurrentUserID;
                tblUserAccount.ModifiedDate = DateTime.Now;
                _entities.Entry(tblUserAccount).State = EntityState.Modified;
                _entities.SaveChanges();

                tblUserProfile.Name = requestModel.Name;
                tblUserProfile.email = requestModel.Email;
                tblUserProfile.ModifiedBy = requestModel.CurrentUserID;
                tblUserProfile.ModifiedDate = DateTime.Now;
                _entities.Entry(tblUserProfile).State = EntityState.Modified;
                _entities.SaveChanges();

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

        [HttpPost]
        [Route("~/api/UserAccount/UserAccountCreate")]
        public async Task<IHttpActionResult> UserAccountCreate(APIRequestModel request)
        {
            var responseModel = new APIResponseModel<APIPGPResponseModel>();

            _entities = new PMASEntities();
            _KeyServerEntities = new PMAS_Backend_KeyServerEntities();
            try
            {
                ApplicationLog.WriteInfo("start check handshaketoken");
                var requestcode = WebUtility.HtmlEncode(request.HandShakeToken);
                if (!AuthenticateUtility.VerifyAuthentication(_entities, requestcode))
                {
                    ApplicationLog.WriteError("fail to check handshaketoken");
                    responseModel.AddError("Authentication Failed.");
                    return Ok(responseModel);
                }
                ApplicationLog.WriteInfo("token check success");
                //ApplicationLog.WriteInfo("request pgp encrypt data :" + request.RequestPGPEncryptString);
                var decryptString = await PGPHelper.PGP_DataDecrypt
                    (request.RequestPGPEncryptString, _entities, _KeyServerEntities);
                var requestModel = JsonConvert.DeserializeObject<UserAccountCreateModel>(decryptString);
                ApplicationLog.WriteInfo("pgp decrypt data :" + decryptString);

                UserAccountModelValidator validator = new UserAccountModelValidator();
                var validatorResult = validator.Validate(requestModel);

                if (!validatorResult.IsValid)
                {
                    ApplicationLog.WriteError(validatorResult.Errors[0].ErrorMessage);
                    responseModel.AddError(validatorResult.Errors[0].ErrorMessage);
                    return Ok(responseModel);
                }
                //check whether loginID already exist or not. to prevent duplication
                bool loginIdExists = _entities.tbl_UserAccount.Any(u => u.LoginID == requestModel.LoginID);
                bool emailExists = _entities.tbl_UserProfile.Any(e => e.email == requestModel.Email);
                if (loginIdExists)
                {
                    ApplicationLog.WriteError("Fail to create new employee user account");
                    responseModel.AddError("LoginID already exists.");
                    return Ok(responseModel);
                } else if (emailExists)
                {
                    //ApplicationLog.WriteError("Fail to create new employee user account");
                    //responseModel.AddError("Email already exists.");
                    //return Ok(responseModel);
                }

                string _NewEncryptPassword = string.Empty;
                byte[] SaltKey = Base.CryptoSecurity.GetSaltKey(16);
                _NewEncryptPassword = Base.CryptoSecurity.HashCode(Base.CryptoSecurity.GetBytes(requestModel.Password), SaltKey);

                Guid newUserID = Guid.NewGuid();
                tbl_UserAccount tblUserAccount = new tbl_UserAccount();
                tblUserAccount.ID = newUserID;
                tblUserAccount.LoginID = requestModel.LoginID;
                tblUserAccount.Password = _NewEncryptPassword;
                tblUserAccount.Saltkey = SaltKey;
                tblUserAccount.Active = true;
                tblUserAccount.IsEnable = true;
                tblUserAccount.CreatedBy = requestModel.CurrentUserID;
                tblUserAccount.CreatedDate = DateTime.Now;

                tbl_UserProfile tblUserProfile = new tbl_UserProfile();
                tblUserProfile.ID = Guid.NewGuid();
                tblUserProfile.UserAccountID = newUserID;
                tblUserProfile.Name = requestModel.Name;
                tblUserProfile.email = requestModel.Email;
                tblUserProfile.Department_Id = requestModel.Department_Id;
                tblUserProfile.Designation_Id = requestModel.Designation_Id;
                tblUserProfile.Rank_Id = requestModel.Rank_Id;
                tblUserProfile.JobDescriptionForm = requestModel.JobDescription;
                tblUserProfile.JoinDate = requestModel.JoinDate;
                tblUserProfile.ReportingOfficer = requestModel.RO_Id;
                tblUserProfile.ConfirmDate = requestModel.ConfirmDate;
                tblUserProfile.LastDayDate = requestModel.LastDayDate;
                tblUserProfile.Active = true;
                tblUserProfile.CreatedBy = requestModel.CurrentUserID;
                tblUserProfile.CreatedDate = DateTime.Now;

                tbl_PasswordHistory tblPasswordHistory = new tbl_PasswordHistory();
                tblPasswordHistory.ID = Guid.NewGuid();
                tblPasswordHistory.UserAccountID = newUserID;
                tblPasswordHistory.OldPassword = _NewEncryptPassword;
                tblPasswordHistory.Saltkey = SaltKey;
                tblPasswordHistory.CreatedDate = DateTime.Now;
                tblPasswordHistory.CreatedBy = requestModel.CurrentUserID;
                tblPasswordHistory.Active = true;

                _entities.tbl_UserAccount.Add(tblUserAccount);
                _entities.tbl_UserProfile.Add(tblUserProfile);
                _entities.tbl_PasswordHistory.Add(tblPasswordHistory);
                _entities.SaveChanges();

                foreach(var userRole in requestModel.Role_Ids)
                {
                    tbl_UserRole tblUserRole = new tbl_UserRole();
                    tblUserRole.UserAccountId = newUserID;
                    tblUserRole.RoleId = userRole;
                    tblUserRole.Active = true;
                    tblUserRole.CreatedDate = DateTime.Now;
                    tblUserRole.CreatedBy = requestModel.CurrentUserID;

                    _entities.tbl_UserRole.Add(tblUserRole);
                    _entities.SaveChanges();
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

        [HttpPost]
        [Route("~/api/UserAccount/UserAccountDelete")]
        public async Task<IHttpActionResult> UserAccountDelete(APIRequestModel request)
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
                var requestModel = JsonConvert.DeserializeObject<UserAccountCreateModel>(decryptString);
                ApplicationLog.WriteInfo("pgp decrypt data :" + decryptString);


                tbl_UserAccount tblUserAccount = _entities.tbl_UserAccount.Where(x => x.ID == requestModel.UserAccountID).SingleOrDefault();

                tblUserAccount.DeletedDate = DateTime.Now;
                tblUserAccount.DeletedBy = requestModel.CurrentUserID;
                tblUserAccount.Active = false;
                _entities.Entry(tblUserAccount).State = EntityState.Modified;
                _entities.SaveChanges();

                tbl_UserProfile tblUserProfile = _entities.tbl_UserProfile
                                                          .Where(x => x.UserAccountID == requestModel.UserAccountID)
                                                          .SingleOrDefault();
                tblUserProfile.Deleted = DateTime.Now;
                tblUserProfile.DeletedBy = requestModel.CurrentUserID;
                tblUserProfile.Active = false;
                _entities.Entry(tblUserProfile).State = EntityState.Modified;
                _entities.SaveChanges();

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