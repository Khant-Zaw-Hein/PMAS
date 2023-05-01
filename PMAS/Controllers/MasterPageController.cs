using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PMAS.Base;
using PMAS.Interfaces;
using PMAS.Repository;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMAS.Models;

namespace PMAS.Controllers
{
    public class MasterPageController : Controller
    {
        // GET: MasterPage
        private readonly ICommonRepository _commonRepository;
        private readonly PMAS_Backend_WebEntities _context;
        public MasterPageController()
        {
            _commonRepository = new CommonRepository();
            _context = new PMAS_Backend_WebEntities();
        }

        public PartialViewResult GetMenuData()
        {
            Guid currentUserID = Guid.Empty;
            //Guid currentUserRoleID = Guid.Empty;
            if (HttpContext.Session["CurrentUserID"] != null)
            {
                currentUserID = Guid.Parse(Convert.ToString(HttpContext.Session["CurrentUserID"]));
            }
            List<Menu_ProgramViewModel> MenuProgramViewModel = new List<Menu_ProgramViewModel>();
            //if (HttpContext.Session["CurrentUserRoleID"] != null)
            //{
            //    currentUserRoleID = Guid.Parse(Convert.ToString(HttpContext.Session["CurrentUserRoleID"]));
            //}

            //currentUserID = Guid.Parse("7851ABD7-F39D-4FA4-8F7E-09A50207A4A4");
            //currentUserRoleID = Guid.Parse("41ACBA37-AC6D-4021-82C7-7B3109D8A935");


            if (Session["MenuProgramViewModel"] == null)
            {
                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetMenuNProgramData;
                    IDModel model = new IDModel();
                    //model.ID = currentUserRoleID;
                    model.ID = currentUserID;
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(model));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {
                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                              (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            MenuProgramViewModel = JsonConvert.DeserializeObject<List<Menu_ProgramViewModel>>(decryptString);
                            Session["MenuProgramViewModel"] = MenuProgramViewModel;
                        }
                    }
                }
            }
            else
            {
                MenuProgramViewModel = (List<Menu_ProgramViewModel>)Session["MenuProgramViewModel"];
            }
            return PartialView("_PartialMenuPage", MenuProgramViewModel);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Response.Cookies.Add(new System.Web.HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Login");
        }

        public ActionResult KeepAlive()
        {
            return Json("KeepAlive");
        }
    }
}
