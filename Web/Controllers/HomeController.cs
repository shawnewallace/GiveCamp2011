using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : ColumbusGiveCamp2011ControllerBase
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Gallery 202/Westerville Artists Unit Administration!";

            return View();
        }

        public ActionResult PublicIndex()
        {
            return View();
        }
    }
}
