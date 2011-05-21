using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ArtistSubTypeController : Controller
    {
        private ColumbusGiveCamp2011Context db = new ColumbusGiveCamp2011Context();

        //
        // GET: /ArtistSubType/

        public ViewResult Index()
        {
            List<ArtistSubTypeModel> models = db.ArtistSubTypes.ToList();
            db.ArtistTypes.Load();

            return View(models);
        }

        //
        // GET: /ArtistSubType/Details/5

        public ViewResult Details(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = db.ArtistSubTypes.Find(id);
            db.ArtistTypes.Load();
            return View(artistsubtypemodel);
        }

        //
        // GET: /ArtistSubType/Create

        public ActionResult Create()
        {
            ViewData["ArtistTypes"] = new SelectList(db.ArtistTypes.ToList(), "Id", "ArtistType");
            return View();
        }

        //
        // POST: /ArtistSubType/Create

        [HttpPost]
        public ActionResult Create(ArtistSubTypeModel artistsubtypemodel)
        {
            if (ModelState.IsValid)
            {
                db.ArtistSubTypes.Add(artistsubtypemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ArtistTypes"] = new SelectList(db.ArtistTypes.ToList(), "Id", "ArtistType");
            return View(artistsubtypemodel);
        }

        //
        // GET: /ArtistSubType/Edit/5

        public ActionResult Edit(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = db.ArtistSubTypes.Find(id);
            ViewData["ArtistTypes"] = new SelectList(db.ArtistTypes.ToList(), "Id", "ArtistType", artistsubtypemodel.ArtistTypeId);
            return View(artistsubtypemodel);
        }

        //
        // POST: /ArtistSubType/Edit/5

        [HttpPost]
        public ActionResult Edit(ArtistSubTypeModel artistsubtypemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistsubtypemodel).State = EntityState.Modified;

                foreach (ArtistModel artist in db.Artists.ToList())
                {
                    if (artist.ArtistSubTypeId == artistsubtypemodel.Id && artist.ArtistTypeId != artistsubtypemodel.ArtistTypeId)
                    {
                        artist.ArtistTypeId = artistsubtypemodel.ArtistTypeId;
                        db.Entry(artist).State = EntityState.Modified;
                    }
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["ArtistTypes"] = new SelectList(db.ArtistTypes.ToList(), "Id", "ArtistType", artistsubtypemodel.ArtistTypeId);
            return View(artistsubtypemodel);
        }

        //
        // GET: /ArtistSubType/Delete/5

        public ActionResult Delete(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = db.ArtistSubTypes.Find(id);
            return View(artistsubtypemodel);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = db.ArtistSubTypes.Find(id);
            db.ArtistSubTypes.Remove(artistsubtypemodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ContentResult AjaxGet(int id)
        {
            IEnumerable<ArtistSubTypeModel> artistSubTypes = db.ArtistSubTypes.ToList().Where(x => x.ArtistTypeId == id);
            StringBuilder sb = new StringBuilder();

            foreach (ArtistSubTypeModel artistSubType in artistSubTypes)
            {
                sb.AppendFormat("<option value='{0}'>{1}</option>", artistSubType.Id, artistSubType.ArtistSubType);
            }

            return Content(sb.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
