using PMAS.Base;
using PMAS.Interfaces;
using PMAS.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PMAS.Repository
{
    public class CommonRepository : ICommonRepository
    {
        public RestResponse CommonApiCall(string apiurl, string encryptString, string handshaketoken, bool isCloud)
        {
            string BaseUrl = string.Empty;
            if (isCloud)
            {
                BaseUrl = ConfigurationManager.AppSettings["CloudApiURL"].ToString();
            }
            else
            {
                //BaseUrl = @"https://localhost:61518/";
                BaseUrl = ConfigurationManager.AppSettings["BackendApiURL"].ToString();
            }

            string api = BaseUrl + apiurl;

            var options = new RestClientOptions(api)
            {
                ThrowOnAnyError = true,
                //Timeout = 500000  // 1 second - thanks to @JohnMc
                MaxTimeout = 500000  // 1 second - thanks to @JohnMc
            };
            var client = new RestClient(options);

            var request = new RestRequest();
            request = new RestRequest(api, Method.Post);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            APIDataSendModel model = new APIDataSendModel
            {
                RequestPGPEncryptString = encryptString,
                HandShakeToken = handshaketoken
            };
            request.AddJsonBody(model);

            RestResponse response = client.Execute(request);
            return response;
        }

        public string GetAuthenticationCode(ref string returnMessage, bool isCloud, PMAS_Backend_WebEntities _context)
        {
            try
            {
                string requestCode = string.Empty;
                string BaseUrl = string.Empty;
                if (isCloud)
                {
                    BaseUrl = ConfigurationManager.AppSettings["CloudApiURL"].ToString();
                }
                else
                {
                    BaseUrl = ConfigurationManager.AppSettings["BackendApiURL"].ToString();
                }

                //string publicKey = ConfigurationManager.AppSettings["thk-puk"].ToString();
                //string privateKey = ConfigurationManager.AppSettings["thk-prk"].ToString();

                string publicKey = string.Empty;
                string privateKey = string.Empty;
                var data = _context.tbl_WebAPIKey.Where(x => x.RequestSource == "WebApi.Database").FirstOrDefault();
                if (data != null)
                {
                    publicKey = data.HandshakeKey;
                    privateKey = data.HashKey;
                }
                string apiUrl = BaseUrl + APIURL.GetRequestAuthCode;
                var client = new RestClient(apiUrl);
                //var request = new RestRequest(apiUrl, Method.POST);
                RequestPublicKeyModel model = new RequestPublicKeyModel();
                model.APIHandshakeKey = publicKey;
                var request = new RestRequest(apiUrl, Method.Post);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(model);
                RestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (Convert.ToString(response.Content) != "" && response.Content != null)
                    {
                        requestCode = Convert.ToString(response.Content);
                        requestCode = requestCode.Replace("\"", string.Empty).Trim();
                        requestCode = CryptoSecurity.getHashData(requestCode + privateKey);
                    }
                    else
                    {
                        returnMessage = response.ErrorMessage.ToString();
                    }
                    return requestCode;
                }
                else
                {
                    //returnMessage = ErrorStatusCode.ApiRequestFail +"";
                    return "cannot connect to api server";
                }
            }
            catch (Exception ex)
            {
                returnMessage = ex.Message;
                return ex.Message.ToString();
            }
        }
    }
}