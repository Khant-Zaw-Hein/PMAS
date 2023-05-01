using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMAS.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/Report/ReportList")]
        public ActionResult ReportList()
        {
            return View();
        }
    }
}