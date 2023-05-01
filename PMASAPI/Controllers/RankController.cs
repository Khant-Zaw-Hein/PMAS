﻿using Newtonsoft.Json;
using PMASAPI.Base;
using PMASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PMASAPI.Controllers
{
    public class RankController : ApiController
    {
        // GET: Rank
        PMASEntities _entities = new PMASEntities();
        PMAS_Backend_KeyServerEntities _KeyServerEntities = new PMAS_Backend_KeyServerEntities();

        [HttpPost]
        [Route("~/api/Rank/GetRankData")]
        public async Task<IHttpActionResult> GetRankData(APIRequestModel request)
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
                IList<tbl_Rank> tblRank = _entities.tbl_Rank.
                                                    Where(x => x.disabled == false).
                                                    OrderBy(x => x.Rank_Id).ToList();
                string returingResult = await PGPHelper.PGP_DataEncrypt(JsonConvert.SerializeObject(tblRank), _entities, _KeyServerEntities);
                responseModel.Response = new APIPGPResponseModel
                {
                    ResponsePGPEncryptString = returingResult,
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