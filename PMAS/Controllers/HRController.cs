using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PMAS.Base;
using PMAS.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMAS.Interfaces;
using PMAS.Repository;
using PagedList;

namespace PMAS.Controllers
{
    public class HRController : Controller
    {

        private readonly ICommonRepository _commonRepository;
        private readonly PMAS_Backend_WebEntities _context;
        public HRController()
        {
            _commonRepository = new CommonRepository();
            _context = new PMAS_Backend_WebEntities();
        }
        // GET: HR

        public ActionResult PartialViewTest(SearchCodeTableViewModel searchModel)
        {
            return PartialView("_ListingPartialView");
        }
        //[HttpPost]
        //public ActionResult SearchDataByPage(SearchCodeTableViewModel searchModel)
        //{
        //    List<CodeTableViewModel> lstCodeTableViewModel = new List<CodeTableViewModel>();
        //    int totalCount = 0;
        //    try
        //    {
        //        searchModel.page = (searchModel.page ?? 1) - 1;
        //        searchModel.pagesize = (searchModel.pagesize ?? 50);
        //        string apiUrl = string.Empty;
        //        string returnMessage = string.Empty;
        //        int returnCode = ErrorStatusCode.SomethingWentWrong;

        //        string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
        //        if (string.IsNullOrEmpty(returnMessage))
        //        {
        //            apiUrl = APIURL.GetCodeTableData;
        //            string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
        //            RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                JObject jObject = JObject.Parse(response.Content);
        //                if ((bool)jObject.SelectToken("Success") == true)
        //                {
        //                    string decryptString = PGPHelper.PGP_DataDecryptForBackend(jObject.SelectToken("Response")
        //                                                    .SelectToken("ResponsePGPEncryptString")
        //                                                    .ToString());

        //                    totalCount = Convert.ToInt32(jObject.SelectToken("Response").SelectToken("TotalCount"));
        //                    lstCodeTableViewModel = JsonConvert.DeserializeObject<List<CodeTableViewModel>>(decryptString);
        //                }
        //                else
        //                {
        //                    var error = jObject.SelectToken("Errors[0]").ToString();
        //                    totalCount = 0;
        //                }
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //    IPagedList<CodeTableViewModel> codeTablelist = new StaticPagedList<CodeTableViewModel>(lstCodeTableViewModel, searchModel.page.Value + 1, searchModel.pagesize.Value, totalCount);
        //    return PartialView("_ListingPartialView", codeTablelist);
        //}

        [HttpGet]
        [Route("/HR/ViewAppraisals")]
        public ActionResult ViewAppraisals()
        {
            return View();
        }
        //[HttpGet]
        //[Route("/HR/ViewEmployees")]
        //public ActionResult ViewEmployees()
        //{
        //    return View();
        //}
        //[HttpGet]
        //[Route("/HR/CreateEmployee")]
        //public ActionResult CreateEmployee()
        //{
        //    return View();
        //}
        [HttpGet]
        [Route("/HR/CreateAppraisal")]
        public ActionResult CreateAppraisal()
        {
            return View();
        }
        [HttpGet]
        [Route("/HR/ViewAppraisalStaff")]
        public ActionResult ViewAppraisalStaff()
        {
            return View();
        }
        [HttpGet]
        [Route("/HR/FormC")]
        public ActionResult FormC()
        {
            return View();
        }
    }
}
