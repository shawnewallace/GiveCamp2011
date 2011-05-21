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
    [Authorize]
    public class ArtistTypeController : Controller
    {
        private ColumbusGiveCamp2011Context db = new ColumbusGiveCamp2011Context();

        //
        // GET: /ArtistType/

        public ViewResult Index()
        {
            return View(db.ArtistTypes.ToList());
        }

        //
        // GET: /ArtistType/Details/5

        public ViewResult Details(int id)
        {
            ArtistTypeModel artisttypemodel = db.ArtistTypes.Find(id);
            return View(artisttypemodel);
        }

        //
        // GET: /ArtistType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ArtistType/Create

        [HttpPost]
        public ActionResult Create(ArtistTypeModel artisttypemodel)
        {
            if (ModelState.IsValid)
            {
                db.ArtistTypes.Add(artisttypemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artisttypemodel);
        }

        //
        // GET: /ArtistType/Edit/5

        public ActionResult Edit(int id)
        {
            ArtistTypeModel artisttypemodel = db.ArtistTypes.Find(id);
            return View(artisttypemodel);
        }

        //
        // POST: /ArtistType/Edit/5

        [HttpPost]
        public ActionResult Edit(ArtistTypeModel artisttypemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artisttypemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artisttypemodel);
        }

        //
        // GET: /ArtistType/Delete/5

        public ActionResult Delete(int id)
        {
            ArtistTypeModel artisttypemodel = db.ArtistTypes.Find(id);
            return View(artisttypemodel);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistTypeModel artisttypemodel = db.ArtistTypes.Find(id);
            db.ArtistTypes.Remove(artisttypemodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
