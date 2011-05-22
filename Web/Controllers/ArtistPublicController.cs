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
                                    Address = (a.Address1.Enabled ? a.Address1.Data : ""),
									Name = a.FirstName + " " + a.LastName,
									Bio = a.Biography,
									Groups = "",
									Awards = "",
									Category = a.ArtistSubType.ArtistSubType,
									EmailAddress = a.Email.Data,
									PhoneNumber = (a.Phone.Enabled ? a.Phone.Data : ""),
									City = a.City.Data,
									State = a.State.Data,
									ZipCode = a.Zip.Data
								 });

            return View(x.First());
        }
	}
}
