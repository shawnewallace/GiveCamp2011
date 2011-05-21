using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class VenueController : Controller
    {
        public ViewResult Index()
        {
            var model = new List<VenueModel>();
            return View(model);
        }
    }
}
