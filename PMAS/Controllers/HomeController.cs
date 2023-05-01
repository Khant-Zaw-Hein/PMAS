using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMAS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // testing Theme
        [HttpGet]
        [Route("~/Home/test")]
        public ActionResult Testing()
        {

            return View();
        }
    }
}