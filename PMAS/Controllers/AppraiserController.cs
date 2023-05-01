using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMAS.Controllers
{
    public class AppraiserController : Controller
    {
        // GET: Appraiser
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/Appraiser/ViewAppraisals")]
        public ActionResult ViewAppraisals()
        {
            return View();
        }
        [HttpGet]
        [Route("/Appraiser/PerformAppraisal")]
        public ActionResult PerformAppraisal()
        {
            return View();
        }
    }
}