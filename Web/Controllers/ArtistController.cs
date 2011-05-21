using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lib.Common;
using Telerik.Web.Mvc;
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
        }

        private IQueryable<ArtistModel> _artists;
        private IQueryable<ArtistModel> Artists
        {
            get { return  _artists ?? Db.Artists.AsQueryable(); }
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
        public ActionResult Create()
        {
            //ViewData["MonthList"] = (new ArtistModel()).Dob.Month.ToSelectList();
            LoadDropDowns(null, null);
            return View();
        } 

        //
        // POST: /Artist/Create
        [HttpPost]
        public ActionResult Create(ArtistModel artistmodel)
        {
            if (ModelState.IsValid)
            {
                Db.Artists.Add(artistmodel);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropDowns(null, null);
            return View(artistmodel);
        }

        //
        // GET: /Artist/Edit/5
        public ActionResult Edit(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            LoadDropDowns(artistmodel.ArtistTypeId, artistmodel.ArtistSubTypeId);
            return View("Create", artistmodel);
        }

        //
        // POST: /Artist/Edit/5

        [HttpPost]
        public ActionResult Edit(ArtistModel artistmodel)
        {
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


    }
}