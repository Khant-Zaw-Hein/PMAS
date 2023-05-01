using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMAS.Controllers
{
    public class AppraiseeController : Controller
    {
        // GET: Appraisee
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/Appraisee/ViewAppraisals")]
        public ActionResult ViewAppraisals()
        {
            return View();
        }
        [HttpGet]
        [Route("/Appraisee/SubmitAppraisal")]
        public ActionResult SubmitAppraisal()
        {
            return View();
        }
    }
}