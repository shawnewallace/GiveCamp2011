using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class VenueController : ColumbusGiveCamp2011ControllerBase
    {
        [Authorize]
        public ViewResult Index()
        {
            var model = Db.Venues
                .Select(v => v)
                .OrderBy(v => v.Name)
                .ToList();

            return View("Index", model);
        }

        //
        // GET: /Venue/Details/5
        [Authorize]
        public ViewResult Details(int id)
        {
            VenueModel model = Db.Venues.Find(id);
            if (model == null || model.Id < 1)
            {
                TempData["BadNumber"] = id;
                return View("NoVenue");
            }
            return View(model);
        }

        //
        // GET: /Venue/Create
        [Authorize]
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                // ** DEBUG
                Session["artistId"] = 1;
                int? iArtistId = (int)Session["artistId"];
                
                if (iArtistId.HasValue)
                {
                    ArtistModel artist = Db.Artists.First(x => x.Id == iArtistId.Value);

                    if (artist.VenueId.HasValue)
                    {
                        return View("VenueAlreadyDefined");
                    }
                }
            }

            return View();
        }

        //
        // POST: /Venue/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(VenueModel model)
        {
            ArtistModel artist = null;

            // ** DEBUG
            Session["artistId"] = 1;
            int? iArtistId = (int?)Session["artistId"];

            if (!User.IsInRole("Admin"))
            {
                if (!iArtistId.HasValue)
                {
                    return View("NotRegistered");
                }
                else
                {
                    artist = Db.Artists.First(x => x.Id == iArtistId.Value);

                    if (artist.VenueId.HasValue)
                    {
                        return View("VenueAlreadyDefined");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                // Gotta do this first to get the identity column.
                Db.Venues.Add(model);
                Db.SaveChanges();

                if (artist != null)
                {
                    artist.VenueId = model.Id;
                    Db.Entry(artist).State = EntityState.Modified;
                    Db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Venue/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            VenueModel model = Db.Venues.Find(id);

            // ** DEBUG
            Session["artistId"] = 1;
            int? iArtistId = (int?)Session["artistId"];

            if (!User.IsInRole("Admin"))
            {
                if (!iArtistId.HasValue)
                {
                    return View("NotRegistered");
                }

                ArtistModel artist = Db.Artists.Find(iArtistId.Value);
                
                if (model.Id != artist.VenueId)
                {
                    return View("NotYourVenue");
                }
            }

            return View(model);
        }

        //
        // POST: /Venue/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(VenueModel model)
        {
            // ** DEBUG
            Session["artistId"] = 1;
            int? iArtistId = (int?)Session["artistId"];

            if (!User.IsInRole("Admin"))
            {
                if (!iArtistId.HasValue)
                {
                    return View("NoVenue");
                }

                ArtistModel artist = Db.Artists.First(x => x.Id == iArtistId.Value);
                
                if (model.Id != artist.VenueId)
                {
                    return View("NotYourVenue");
                }
            }

            if (ModelState.IsValid)
            {
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /Venue/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            VenueModel model = Db.Venues.Find(id);
            return View(model);
        }

        //
        // POST: /Venue/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueModel model = Db.Venues.Find(id);

            foreach (ArtistModel artist in Db.Artists.Where(x => x.VenueId == id))
            {
                artist.VenueId = null;
                Db.Entry(artist).State = EntityState.Modified;
            }

            Db.Venues.Remove(model);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult Find(string searchTerms = "")
        {
            var terms = searchTerms.Split(' ');
            var venues = Db.Venues.AsEnumerable<VenueModel>();

            if (searchTerms != string.Empty)
            {
                List<VenueModel> result = new List<VenueModel>();

                foreach (var term in terms)
                {
                    venues = venues.Where(v => v.Name.Contains(term)
                        || v.Address.Contains(term));
                    result.AddRange(venues);
                }
                venues = result;
            }

            int count = venues.Count();

            return Json(new
            {
                Venues = venues.Select(v => v).Distinct(),
                Count = count
            },JsonRequestBehavior.AllowGet);
        }
    }
}
