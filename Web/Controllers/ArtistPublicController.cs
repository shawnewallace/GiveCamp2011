using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ArtistPublicController : ColumbusGiveCamp2011ControllerBase
    {
        //
        // GET: /ArtistPublic/

        public PartialViewResult CoverFlow()
        {
            return PartialView("_CoverFlow", GetAllCoverFlowArt());
        }

        public ViewResult Profile(int id)
        {
            var x = Db.Artists
                .Where(a => a.Id == id)
                .Select(a => new ProfileModel
                                 {
                                     Address = (a.Address1.Enabled ? a.Address1.Data : "")

                                 });

            return View();
        }
    }
}
