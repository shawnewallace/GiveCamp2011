using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ArtistController : ColumbusGiveCamp2011ControllerBase
    {
        private void LoadDropDowns(int? id)
        {
            List<ArtistTypeModel> artistTypes = Db.ArtistTypes.ToList();

            if (artistTypes.Count < 1)
            {
                ViewData["ArtistTypes"] = new SelectList(new List<ArtistTypeModel>());
                ViewData["ArtistSubTypes"] = new SelectList(new List<ArtistSubTypeModel>());
            }
            else
            {
                if (!id.HasValue) id = artistTypes[0].Id;
                ViewData["ArtistTypes"] = new SelectList(artistTypes, "Id", "ArtistType", id);

                IEnumerable<ArtistSubTypeModel> subTypes = Db.ArtistSubTypes.ToList().Where(x => x.ArtistTypeId == id);
                ViewData["ArtistSubTypes"] = new SelectList(subTypes, "Id", "ArtistSubType");
            }
        }

        //
        // GET: /Artist/

        public ViewResult Index()
        {
            List<ArtistModel> artists = Db.Artists.ToList();
            LoadDropDowns(null);
            
            return View(Db.Artists.ToList());
        }

        //
        // GET: /Artist/Details/5

        public ViewResult Details(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            LoadDropDowns(null);
            return View(artistmodel);
        }

        //
        // GET: /Artist/Create

        public ActionResult Create()
        {
            LoadDropDowns(null);
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

            LoadDropDowns(null);
            return View(artistmodel);
        }
        
        //
        // GET: /Artist/Edit/5
 
        public ActionResult Edit(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            LoadDropDowns(id);
            return View(artistmodel);
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

            LoadDropDowns(artistmodel.Id);
            return View(artistmodel);
        }

        //
        // GET: /Artist/Delete/5
 
        public ActionResult Delete(int id)
        {
            ArtistModel artistmodel = Db.Artists.Find(id);
            return View(artistmodel);
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