﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lib.Common;
using Telerik.Web.Mvc;
using Web.Controllers;
using Web.Models;

namespace Web.Controllers
{
    public class ArtistController : ColumbusGiveCamp2011ControllerBase
    {
        private void LoadDropDowns(int? artistTypeId, int? artistSubTypeId)
        {
            List<ArtistTypeModel> artistTypes = Db.ArtistTypes.ToList();

            if (artistTypes.Count < 1)
            {
                ViewData["ArtistTypes"] = new SelectList(new List<ArtistTypeModel>());
                ViewData["ArtistSubTypes"] = new SelectList(new List<ArtistSubTypeModel>());
            }
            else
            {
                if (!artistTypeId.HasValue) artistTypeId = artistTypes[0].Id;
                ViewData["ArtistTypes"] = new SelectList(artistTypes, "Id", "ArtistType", artistTypeId);

                IEnumerable<ArtistSubTypeModel> subTypes = Db.ArtistSubTypes.ToList().Where(x => x.ArtistTypeId == artistTypeId);

                if (artistSubTypeId.HasValue)
                    ViewData["ArtistSubTypes"] = new SelectList(subTypes, "Id", "ArtistSubType", artistSubTypeId.Value);
                else
                    ViewData["ArtistSubTypes"] = new SelectList(subTypes, "Id", "ArtistSubType");
            }
            ViewData["MonthList"] = (new ArtistModel()).Dob.Month.ToSelectList();
            ViewData["Venues"] = new SelectList(Db.Venues.ToList(), "Id", "Name");
        }

        private IQueryable<ArtistModel> _artists;
        private IQueryable<ArtistModel> Artists
        {
            get { return _artists ?? Db.Artists.AsQueryable(); }
        }

        [Authorize(Roles = "Admin")]
        public ViewResult UnapprovedArt()
        {
            return View(GetUnapprovedArt());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult ApproveImage(string imageToApprove)
        {
            ApproveSubmittedImage(imageToApprove);

            return View("UnapprovedArt", GetUnapprovedArt());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult RejectImage(string imageToReject)
        {
            RejectSubmittedImage(imageToReject);

            return View("UnapprovedArt", GetUnapprovedArt());
        }


        //
        // GET: /Artist/

        public ViewResult Index()
        {
            //List<ArtistModel> artists = Db.Artists.ToList();
            LoadDropDowns(null, null);

            return View(Artists);
        }

        // GET: /Artist/Details/5
        public ViewResult Details(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            LoadDropDowns(null, null);
            return View(artistmodel);
        }

        //
        // GET: /Artist/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewData["CreateActionName"] = User.IsInRole("Admin") ? "Create" : "CreatePublic";
            ViewData["CreateControllerName"] = "Artist";
            LoadDropDowns(null, null);
            return View();
        }

        //
        // POST: /Artist/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(ArtistModel artistmodel)
        {
            if (ModelState.IsValid)
            {
                artistmodel.UserId = User.Identity.Name;
                Db.Artists.Add(artistmodel);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropDowns(null, null);
            return View("Create", User.IsInRole("Admin") ? "_Layout" : "_PublicLayout", artistmodel);
        }


        //
        // GET: /Artist/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);

            if (
                !User.IsInRole("Admin")
                && artistmodel.UserId != User.Identity.Name
            )
            {
                return View("NotYourArtist");
            }

            LoadDropDowns(artistmodel.ArtistTypeId, artistmodel.ArtistSubTypeId);
            return View("Create", artistmodel);
        }

        //
        // POST: /Artist/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(ArtistModel artistmodel)
        {
            if (
                !User.IsInRole("Admin")
                && artistmodel.UserId != User.Identity.Name
            )
            {
                return View("NotYourArtist");
            }

            if (ModelState.IsValid)
            {
                Db.Entry(artistmodel).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropDowns(artistmodel.ArtistTypeId, artistmodel.ArtistSubTypeId);
            return View(artistmodel);
        }

        //
        // GET: /Artist/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            Db.Artists.Remove(artistmodel);
            Db.SaveChanges();
            return RedirectToAction("Index");
            //return View(artistmodel);
        }

        //
        // POST: /Artist/Delete/5

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            Db.Artists.Remove(artistmodel);
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
            //firstname
            //lastname
            //type
            //category

            var terms = searchTerms.Split(' ');
            var artists = Db.Artists.AsEnumerable();
            var results = new List<ArtistModel>();

            foreach (var term in terms)
            {
                results.AddRange(

                    Db.Artists.Where(a => a.FirstName.Contains(term)
                        || a.LastName.Contains(term)
                        || a.ArtistType.ArtistType.Contains(term)
                        || a.ArtistSubType.ArtistSubType.Contains(term)
                    ).ToList()

                    );
            }

            results = results.Select(r => r).Distinct().ToList();

            int count = results.Count();

            return Json(new
            {
                Artists = results,
                Count = count
            }, JsonRequestBehavior.AllowGet);
        }

    }
}