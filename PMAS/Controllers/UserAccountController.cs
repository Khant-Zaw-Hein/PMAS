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
using PMAS.Validators;

namespace PMAS.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount

        //[HttpGet]
        //public ActionResult Listing()
        //{
        //    return View();
        //}

        private readonly ICommonRepository _commonRepository;
        private readonly PMAS_Backend_WebEntities _context;
        public UserAccountController()
        {
            _commonRepository = new CommonRepository();
            _context = new PMAS_Backend_WebEntities();
        }
        public ActionResult Listing()
        {
            List<UserAccountViewModel> list = new List<UserAccountViewModel>();
            try
            {
                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetUserAccountData;
                    //string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, "", requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            list = JsonConvert.DeserializeObject<List<UserAccountViewModel>>(decryptString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(list);
        }
        public ActionResult Create()
        {
            // 1. Get departmentList
            List<SelectListItem> DepartmentList = GetDepartmentList(string.Empty);
            ViewBag.DepartmentList = DepartmentList;
            // 2. GetDesignationList
            List<SelectListItem> DesignationList = GetDesignationList(string.Empty);
            ViewBag.DesignationList = DesignationList;
            // 3. GetRankList
            List<SelectListItem> RankList = GetRankList(string.Empty);
            ViewBag.RankList = RankList;
            // 4. GetRoleList (optional) 
            List<SelectListItem> RoleList = GetRoleList(string.Empty);
            ViewBag.RoleList = RoleList;
            //5. Get Reporting Officers
            List<SelectListItem> RO_List = GetReportingOfficerList(string.Empty);
            ViewBag.RO_List = RO_List;

            return View();
        }

        [HttpPost]
        public ActionResult Delete(string detailID)
        {
            string jsonResult;
            //Guid CurrentUserID = Guid.Parse(Convert.ToString(HttpContext.Session["CurrentUserID"]));
            Guid CurrentUserID = Guid.Parse("A73DEFA9-2AAE-4BF3-BCB7-4E40D9E365DF");
            ResponseViewModel responseObj = new ResponseViewModel();
            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.UserAccountDelete;
                    UserAccountCreateModel model = new UserAccountCreateModel();
                    model.UserAccountID = Guid.Parse(detailID);
                    model.CurrentUserID = CurrentUserID;
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(model));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            responseObj.Message = "UserAccount Delete Successful.";
                            responseObj.Status = "Success";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                        else
                        {
                            var error = jObject.SelectToken("Errors[0]").ToString();
                            responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + error;
                            responseObj.Status = "Fail";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                    }
                    else
                    {
                        responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + "Cannot connect to api";
                        responseObj.Status = "Fail";
                        jsonResult = JsonConvert.SerializeObject(responseObj);
                        return Json(jsonResult);
                    }
                }
                else
                {
                    responseObj.Message = returnMessage;
                    responseObj.Status = "Fail";
                    jsonResult = JsonConvert.SerializeObject(responseObj);
                    return Json(jsonResult);
                }

            }

            catch (Exception ex)
            {
                responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + ex.Message;
                responseObj.Status = "Fail";
                jsonResult = JsonConvert.SerializeObject(responseObj);
                return Json(jsonResult);
            }
        }
        public ActionResult Edit(Guid detailID)
        {
            UserAccountViewModel userAccountViewModel = new UserAccountViewModel();
            try
            {
                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetUserAccountById;
                    IDModel model = new IDModel();
                    model.ID = detailID;
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(model));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            userAccountViewModel = JsonConvert.DeserializeObject<UserAccountViewModel>(decryptString);
                            //List<SelectListItem> SelectedUserRoleList = GetUserRoleList(Convert.ToString(userAccountViewModel.UserRoleID));
                            //ViewBag.SelectedUserRoleList = SelectedUserRoleList;
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }


            return View(userAccountViewModel);
        }
        [HttpPost]
        public ActionResult UserAccount_EditSave(UserAccountViewModel SaveDataObj)
        {
            string jsonResult;
            Guid CurrentUserID = Guid.Parse(Convert.ToString(HttpContext.Session["CurrentUserID"]));
            ResponseViewModel responseObj = new ResponseViewModel();
            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.UserAccountEdit;
                    UserAccountCreateModel model = new UserAccountCreateModel();
                    model.UserAccountID = SaveDataObj.UserAccountID;
                    model.CurrentUserID = CurrentUserID;
                    model.LoginID = SaveDataObj.LoginID;
                    model.Email = SaveDataObj.Email;
                    model.Name = SaveDataObj.Name;
                    model.Password = SaveDataObj.Password;
                    //model.UserRoleID = SaveDataObj.UserRoleID;
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(model));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            responseObj.Message = "UserAccount Edit Successful.";
                            responseObj.Status = "Success";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                        else
                        {
                            var error = jObject.SelectToken("Errors[0]").ToString();
                            responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + error;
                            responseObj.Status = "Fail";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                    }
                    else
                    {

                        responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + "Cannot connect to api";
                        responseObj.Status = "Fail";
                        jsonResult = JsonConvert.SerializeObject(responseObj);
                        return Json(jsonResult);

                    }
                }
                else
                {
                    responseObj.Message = returnMessage;
                    responseObj.Status = "Fail";
                    jsonResult = JsonConvert.SerializeObject(responseObj);
                    return Json(jsonResult);
                }

            }

            catch (Exception ex)
            {
                responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + ex.Message;
                responseObj.Status = "Fail";
                jsonResult = JsonConvert.SerializeObject(responseObj);
                return Json(jsonResult);
            }
        }
        [HttpPost]
        public ActionResult SaveData(UserAccountViewModel SaveDataObj)
        {
            string jsonResult;
            Guid CurrentUserID = Guid.Parse(Convert.ToString(HttpContext.Session["CurrentUserID"]));
            //Guid CurrentUserID = Guid.Parse("6d25639d-ce23-4032-b4e2-70aa7e2c0a7d");
            //Guid a = (Guid)HttpContext.Session["CurrentUserID"];

            ResponseViewModel responseObj = new ResponseViewModel();

            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.UserAccountCreate;

                    UserAccountCreateModel model = new UserAccountCreateModel();
                    model.UserAccountID = SaveDataObj.UserAccountID;
                    model.CurrentUserID = CurrentUserID;
                    model.LoginID = SaveDataObj.LoginID;
                    model.Password = "Neo#tech";
                    model.Name = SaveDataObj.Name;
                    model.Email = SaveDataObj.Email;
                    model.Department_Id = SaveDataObj.Department_Id;
                    model.Designation_Id = SaveDataObj.Designation_Id;
                    model.Rank_Id = SaveDataObj.Rank_Id;
                    model.Role_Ids = SaveDataObj.Role_Ids;
                    model.RO_Id = SaveDataObj.RO_Id;
                    model.JobDescription = SaveDataObj.JobDescription;
                    model.JoinDate = SaveDataObj.JoinDate;
                    model.ConfirmDate = SaveDataObj.ConfirmDate;
                    model.LastDayDate = SaveDataObj.LastDayDate;
                    // Continue

                    UserAccountModelValidator validator = new UserAccountModelValidator();
                    var validatorResult = validator.Validate(model);

                    if (!validatorResult.IsValid)
                    {
                        responseObj.Message = validatorResult.Errors[0].ErrorMessage;
                        responseObj.Status = "Fail";
                        jsonResult = JsonConvert.SerializeObject(responseObj);
                        return Json(jsonResult);
                    }
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(model));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            responseObj.Message = "UserAccount Create Successful.";
                            responseObj.Status = "Success";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                        else
                        {
                            var error = jObject.SelectToken("Errors[0]").ToString();
                            //responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + error;
                            responseObj.Message = error;
                            responseObj.Status = "Fail";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                    }
                    else
                    {

                        responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + "Cannot connect to api";
                        responseObj.Status = "Fail";
                        jsonResult = JsonConvert.SerializeObject(responseObj);
                        return Json(jsonResult);

                    }
                }
                else
                {
                    responseObj.Message = returnMessage;
                    responseObj.Status = "Fail";
                    jsonResult = JsonConvert.SerializeObject(responseObj);
                    return Json(jsonResult);
                }

            }

            catch (Exception ex)
            {
                responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + ex.Message;
                responseObj.Status = "Fail";
                jsonResult = JsonConvert.SerializeObject(responseObj);
                return Json(jsonResult);
            }
        }

        [HttpPost]
        public ActionResult UserAccount_Delete(string detailID)
        {
            string jsonResult;
            Guid CurrentUserID = Guid.Parse(Convert.ToString(HttpContext.Session["CurrentUserID"]));
            //Guid CurrentUserID = Guid.Parse("A73DEFA9-2AAE-4BF3-BCB7-4E40D9E365DF");
            ResponseViewModel responseObj = new ResponseViewModel();
            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.UserAccountDelete;
                    UserAccountCreateModel model = new UserAccountCreateModel();
                    model.UserAccountID = Guid.Parse(detailID);
                    model.CurrentUserID = CurrentUserID;
                    string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(model));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, ret, requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            responseObj.Message = "UserAccount Delete Successful.";
                            responseObj.Status = "Success";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                        else
                        {
                            var error = jObject.SelectToken("Errors[0]").ToString();
                            responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + error;
                            responseObj.Status = "Fail";
                            jsonResult = JsonConvert.SerializeObject(responseObj);
                            return Json(jsonResult);

                        }
                    }
                    else
                    {
                        responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + "Cannot connect to api";
                        responseObj.Status = "Fail";
                        jsonResult = JsonConvert.SerializeObject(responseObj);
                        return Json(jsonResult);
                    }
                }
                else
                {
                    responseObj.Message = returnMessage;
                    responseObj.Status = "Fail";
                    jsonResult = JsonConvert.SerializeObject(responseObj);
                    return Json(jsonResult);
                }

            }

            catch (Exception ex)
            {
                responseObj.Message = "Process failed. Please contact to Seacare IT support team. Error message : " + ex.Message;
                responseObj.Status = "Fail";
                jsonResult = JsonConvert.SerializeObject(responseObj);
                return Json(jsonResult);
            }
        }

        #region CustomizeMethod

        private List<SelectListItem> GetDepartmentList(string defaultselectedID)
        {
            List<SelectListItem> SelectItems = new List<SelectListItem>
            {
                new SelectListItem{Text = "-- Select --", Value = ""}
            };
            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetDepartmentData;
                    //string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, "", requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            List<tbl_Department> DeptList = JsonConvert.DeserializeObject<List<tbl_Department>>(decryptString);
                            if (DeptList.Count > 0)
                            {
                                for (int i = 0; i < DeptList.Count; i++)
                                {
                                    string Value = Convert.ToString(DeptList[i].Department_Id);
                                    string textValue = DeptList[i].department;

                                    if (defaultselectedID != string.Empty || defaultselectedID == Value)
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value, Selected = true };
                                        SelectItems.Add(item);
                                    }
                                    else
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value };
                                        SelectItems.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return SelectItems;
        }
        private List<SelectListItem> GetDesignationList(string defaultselectedID)
        {
            List<SelectListItem> SelectItems = new List<SelectListItem>
            {
                new SelectListItem{Text = "-- Select --", Value = ""}
            };
            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetDesignationData;
                    //string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, "", requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            List<tbl_Designation> DesignationList = JsonConvert.DeserializeObject<List<tbl_Designation>>(decryptString);
                            if (DesignationList.Count > 0)
                            {
                                for (int i = 0; i < DesignationList.Count; i++)
                                {
                                    string Value = Convert.ToString(DesignationList[i].Designation_Id);
                                    string textValue = DesignationList[i].designation;

                                    if (defaultselectedID != string.Empty || defaultselectedID == Value)
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value, Selected = true };
                                        SelectItems.Add(item);
                                    }
                                    else
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value };
                                        SelectItems.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return SelectItems;
        }
        private List<SelectListItem> GetRankList(string defaultselectedID)
        {
            List<SelectListItem> SelectItems = new List<SelectListItem>
            {
                new SelectListItem{Text = "-- Select --", Value = ""}
            };
            try
            {

                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetRankData;
                    //string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, "", requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            List<tbl_Rank> RankList = JsonConvert.DeserializeObject<List<tbl_Rank>>(decryptString);
                            if (RankList.Count > 0)
                            {
                                for (int i = 0; i < RankList.Count; i++)
                                {
                                    string Value = Convert.ToString(RankList[i].Rank_Id);
                                    string textValue = RankList[i].rank;

                                    if (defaultselectedID != string.Empty || defaultselectedID == Value)
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value, Selected = true };
                                        SelectItems.Add(item);
                                    }
                                    else
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value };
                                        SelectItems.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return SelectItems;
        }
        private List<SelectListItem> GetRoleList(string defaultselectedID)
        {
            List<SelectListItem> SelectItems = new List<SelectListItem>();

            try
            {
                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetRoleData;
                    //string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, "", requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            List<tbl_Role> RoleList = JsonConvert.DeserializeObject<List<tbl_Role>>(decryptString);
                            if (RoleList.Count > 0)
                            {
                                for (int i = 0; i < RoleList.Count; i++)
                                {
                                    string Value = Convert.ToString(RoleList[i].ID);
                                    string textValue = RoleList[i].Name;

                                    if (defaultselectedID != string.Empty || defaultselectedID == Value)
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value, Selected = true };
                                        SelectItems.Add(item);
                                    }
                                    else
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value };
                                        SelectItems.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return SelectItems;
        }

        private List<SelectListItem> GetReportingOfficerList(string defaultselectedID)
        {
            List<SelectListItem> SelectItems = new List<SelectListItem>
            {
                new SelectListItem{Text = "-- Select --", Value = ""}
            };
            try
            {
                string apiUrl = string.Empty;
                string returnMessage = string.Empty;
                int returnCode = ErrorStatusCode.SomethingWentWrong;
                string requestCode = _commonRepository.GetAuthenticationCode(ref returnMessage, false, _context);
                if (string.IsNullOrEmpty(returnMessage))
                {
                    apiUrl = APIURL.GetUserAccountData;
                    //string ret = PGPHelper.PGP_DataEncryptForBackend(JsonConvert.SerializeObject(searchModel));
                    RestResponse response = _commonRepository.CommonApiCall(apiUrl, "", requestCode, false);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        JObject jObject = JObject.Parse(response.Content);

                        if ((bool)jObject.SelectToken("Success") == true)
                        {

                            string decryptString = PGPHelper.PGP_DataDecryptForBackend
                             (jObject.SelectToken("Response").SelectToken("ResponsePGPEncryptString").ToString());
                            List<UserAccountViewModel> RO_List = JsonConvert.DeserializeObject<List<UserAccountViewModel>>(decryptString);
                            if (RO_List.Count > 0)
                            {
                                for (int i = 0; i < RO_List.Count; i++)
                                {
                                    string Value = Convert.ToString(RO_List[i].UserAccountID);
                                    string textValue = RO_List[i].Name;

                                    if (defaultselectedID != string.Empty || defaultselectedID == Value)
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value, Selected = true };
                                        SelectItems.Add(item);
                                    }
                                    else
                                    {
                                        SelectListItem item = new SelectListItem { Text = textValue, Value = Value };
                                        SelectItems.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return SelectItems;
        }
        #endregion

    }
}