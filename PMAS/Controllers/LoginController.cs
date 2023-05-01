using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PMAS.Base;
using PMAS.Interfaces;
using PMAS.Models;
using PMAS.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMAS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICommonRepository _commonRepository;
        private readonly PMAS_Backend_WebEntities _context;
        public LoginController()
        {
            _commonRepository = new CommonRepository();
            _context = new PMAS_Backend_WebEntities();
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("~/Login/Login")]
        public ActionResult Login(string loginID, string password)
        {
            LoginModel.Response user = new LoginModel.Response();
            try
            {
                string apiUrl = string.Empty;
                string returnMessage = string.Empty;

                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.login;
                    LoginModel.Request loginModelRequest = new LoginModel.Request();
                    loginModelRequest.LoginID = loginID;
                    loginModelRequest.Password = password;
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(loginModelRequest));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {
                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                            (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());

                            user = JsonConvert.DeserializeObject<LoginModel.Response>(decryptString);
                            HttpContext.Session["CurrentUserID"] = Convert.ToString(user.UserAccountID);
                            HttpContext.Session["CurrentUserName"] = Convert.ToString(user.Name);
                            HttpContext.Session["CurrentUserEmail"] = Convert.ToString(user.Email);
                            //HttpContext.Session["CurrentUserRoleID"] = Convert.ToString(user.UserRoleID);
                            HttpContext.Session["CurrentLoginID"] = Convert.ToString(user.LoginID);
                        }
                        else
                        {
                            var error = jObject.SelectToken("Errors[0]").ToString();
                            if (error == "Need password change")
                            {
                                string decryptString = jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString();

                                user = JsonConvert.DeserializeObject<LoginModel.Response>(decryptString);
                                HttpContext.Session["CurrentUserID"] = Convert.ToString(user.UserAccountID);
                                return View("forcedpasswordchange");
                            }
                            else
                            {
                                ViewBag.LoginResponseMsg = error;
                                return View("index");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.LoginResponseMsg = "Cannot connect to api.";
                        return View("index");
                    }
                }
                else
                {
                    ViewBag.LoginResponseMsg = returnMessage;
                    return View("index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.LoginResponseMsg = ex.Message.ToString();
                return View("index");
            }
            return RedirectToAction("index", "Home");
        }
    }
}
