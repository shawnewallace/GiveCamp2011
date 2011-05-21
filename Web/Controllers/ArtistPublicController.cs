using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


    }
}
