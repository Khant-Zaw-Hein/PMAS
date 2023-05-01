using Newtonsoft.Json;
using PMASAPI.Base;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace PMASAPI.Controllers
{
    public class ProgramController : ApiController
    {
        // GET: Program
        PMASEntities _entities = new PMASEntities();
        PMAS_Backend_KeyServerEntities _KeyServerEntities = new PMAS_Backend_KeyServerEntities();

        [HttpPost]
        [Route("~/api/Program/GetMenuNProgramData")]
        public async Task<IHttpActionResult> GetMenuNProgramData(APIRequestModel request)
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
                ApplicationLog.WriteInfo("request pgp encrypt data :" + request.RequestPGPEncryptString);
                var decryptString = await PGPHelper.PGP_DataDecrypt
                    (request.RequestPGPEncryptString, _entities, _KeyServerEntities);
                // -- currentUserID(UserAccountID) in IDModel --
                var searchModel = JsonConvert.DeserializeObject<IDModel>(decryptString);
                ApplicationLog.WriteInfo("pgp decrypt data :" + decryptString);

                List<Menu_ProgramViewModel> MenuProgramViewModel = new List<Menu_ProgramViewModel>();

                MenuProgramViewModel = ((from tblRMP in _entities.tbl_RoleMenuPermission
                                         join tblRole in _entities.tbl_Role on tblRMP.RoleID equals tblRole.ID
                                         join tblUserRole in _entities.tbl_UserRole on tblRole.ID equals tblUserRole.RoleId
                                         join tblMenu in _entities.tbl_Menu on tblRMP.MenuID equals tblMenu.ID
                                         //join tblProgram in _entities.tbl_Program on tblMenu.ProgramID equals tblProgram.ID
                                         join tblProgram in _entities.tbl_Program
                                         on tblMenu.ProgramID equals tblProgram.ID into MP
                                         from MenuProgram in MP.DefaultIfEmpty()
                                         where tblUserRole.UserAccountId == searchModel.ID

                                         select new Menu_ProgramViewModel
                                        {
                                            MenuID = tblMenu.ID,
                                            MenuName = tblMenu.MenuName,
                                            MenuCategory = tblMenu.MenuCategory,
                                            SerialIndexNo = tblMenu.SortIndexSerialNo,
                                            ParentMenuID = (Guid)(tblMenu.ParentMenuID == null ? Guid.Empty : tblMenu.ParentMenuID),
                                            MenuLevel = tblMenu.MenuLevel,
                                            programID = (Guid)(tblMenu.ProgramID == null ? Guid.Empty : tblMenu.ProgramID),
                                            ProgramURL = MenuProgram.ProgramURL
                                         }).Distinct().ToList());

                string ret = await PGPHelper.PGP_DataEncrypt(JsonConvert.SerializeObject(MenuProgramViewModel), _entities, _KeyServerEntities);
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
    }
}