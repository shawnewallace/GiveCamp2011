﻿using System;
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
            ViewBag.Message = "Welcome to Gallery 202/Westerville Artists Unit Administration!";

            return View();
        }

        public ActionResult PublicIndex()
        {
            return View();
        }

        public JsonResult Search(string term = "")
        {
            var splitTerms = term.Split(' ');

            var artists = FindArtists(splitTerms);
            var venues = FindVenues(splitTerms);

            var result = artists.Select(artist => new SearchResult(artist)).ToList();
            result.AddRange(venues.Select(venue => new SearchResult(venue)));

            return Json(new { Results = result.OrderBy(r => r.Name).Select(r => r) }, JsonRequestBehavior.AllowGet );
        }

        private IEnumerable<VenueModel> FindVenues(IEnumerable<string> searchTerms)
        {
            var result = new List<VenueModel>();
            var venues = Db.Venues.AsEnumerable<VenueModel>();

            foreach (var term in searchTerms)
            {
                venues = venues.Where(v => v.Name.Contains(term)
                    || v.Address.Contains(term));
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

                    Db.Artists.Where(a => a.FirstName.Contains(term)
                        || a.LastName.Contains(term)
                        || a.ArtistType.ArtistType.Contains(term)
                        || a.ArtistSubType.ArtistSubType.Contains(term)
                    ).ToList()

                    );
            }

            return results.Select(r => r).Distinct().ToList();
        }
    }
}
