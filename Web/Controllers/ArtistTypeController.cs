using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ArtistTypeController : ColumbusGiveCamp2011ControllerBase
    {
        //
        // GET: /ArtistType/
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(Db.ArtistTypes.ToList());
        }

        //
        // GET: /ArtistType/Details/5
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            ArtistTypeModel artisttypemodel = Db.ArtistTypes.Find(id);
            return View(artisttypemodel);
        }

        public PartialViewResult GetCategoryTree()
        {
            var model = Db
                .ArtistTypes
                .OrderBy(t => t.ArtistType)
                .Select(t =>
                   new BagResult
                   {
                       Id = t.Id,
                       Name = t.ArtistType
                   }).ToList();

            model.ForEach(m => m.Children = Db.ArtistSubTypes
                       .Where(a => a.ArtistTypeId == m.Id)
                       .OrderBy(a => a.ArtistSubType)
                       .Select(a =>
                           new BagResult
                           {
                               Id = a.Id,
                               Name = a.ArtistSubType
                           }).ToList());

            return PartialView("_CategoryTree", model);
        }

        //
        // GET: /ArtistType/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ArtistType/Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(ArtistTypeModel artisttypemodel)
        {
            if (ModelState.IsValid)
            {
                Db.ArtistTypes.Add(artisttypemodel);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artisttypemodel);
        }

        //
        // GET: /ArtistType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ArtistTypeModel artisttypemodel = Db.ArtistTypes.Find(id);
            return View(artisttypemodel);
        }

        //
        // POST: /ArtistType/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ArtistTypeModel artisttypemodel)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(artisttypemodel).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artisttypemodel);
        }

        //
        // GET: /ArtistType/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            ArtistTypeModel artisttypemodel = Db.ArtistTypes.Find(id);
            return View(artisttypemodel);
        }

        //
        // POST: /Artist/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistTypeModel artisttypemodel = Db.ArtistTypes.Find(id);
            Db.ArtistTypes.Remove(artisttypemodel);
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
