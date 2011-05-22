using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : ColumbusGiveCamp2011ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Gallery 202/Westerville Artists Unite Administration!";

            return View();
        }

        public ActionResult PublicIndex()
        {
            return View();
        }

        public JsonResult Search(string term = "")
        {
            var splitTerms = term.ToUpper().Split(' ');

            var artists = FindArtists(splitTerms);
            var venues = FindVenues(splitTerms);

            var result = artists.Select(artist => new SearchResult(artist)).ToList();
            result.AddRange(venues.Select(venue => new SearchResult(venue)));

            return Json(new { Results = result.OrderBy(r => r.Name).Select(r => r) }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SearchType(int id)
        {
            var results = (from a in Db.Artists
                           where a.ArtistType.Id.Equals(id)
                           orderby a.LastName
                           orderby a.FirstName
                           select new SearchResult(a));

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchCategory(int id)
        {
            var results = (from a in Db.Artists
                          where a.ArtistSubType.Id.Equals(id)
                          orderby a.LastName
                          orderby a.FirstName
                          select new SearchResult(a));

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<VenueModel> FindVenues(IEnumerable<string> searchTerms)
        {
            var result = new List<VenueModel>();
            var venues = Db.Venues.AsEnumerable<VenueModel>();

            foreach (var term in searchTerms)
            {
                venues = venues.Where(v => v.Name.ToUpper().Contains(term)
                    || v.Address.ToUpper().Contains(term));
                result.AddRange(venues);
            }
            return result;
        }

        private IEnumerable<ArtistModel> FindArtists(IEnumerable<string> searchTerms)
        {
            var results = new List<ArtistModel>();

            foreach (var term in searchTerms)
            {
                results.AddRange(

                    Db.Artists.Where(a => a.FirstName.ToUpper().Contains(term)
                        || a.LastName.ToUpper().Contains(term)
                        || a.ArtistType.ArtistType.ToUpper().Contains(term)
                        || a.ArtistSubType.ArtistSubType.ToUpper().Contains(term)
                    ).ToList()

                    );
            }

            return results.Select(r => r).Distinct().ToList();
        }
    }
}
