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
    [Authorize(Roles="Admin")]
    public class ArtistSubTypeController : ColumbusGiveCamp2011ControllerBase
    {
        //
        // GET: /ArtistSubType/

        public ViewResult Index()
        {
            List<ArtistSubTypeModel> models = Db.ArtistSubTypes.ToList();
            Db.ArtistTypes.Load();

            return View(models);
        }

        //
        // GET: /ArtistSubType/Details/5

        public ViewResult Details(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = Db.ArtistSubTypes.Find(id);
            Db.ArtistTypes.Load();
            return View(artistsubtypemodel);
        }

        //
        // GET: /ArtistSubType/Create

        public ActionResult Create()
        {
            ViewData["ArtistTypes"] = new SelectList(Db.ArtistTypes.ToList(), "Id", "ArtistType");
            return View();
        }

        //
        // POST: /ArtistSubType/Create

        [HttpPost]
        public ActionResult Create(ArtistSubTypeModel artistsubtypemodel)
        {
            if (ModelState.IsValid)
            {
                Db.ArtistSubTypes.Add(artistsubtypemodel);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["ArtistTypes"] = new SelectList(Db.ArtistTypes.ToList(), "Id", "ArtistType");
            return View(artistsubtypemodel);
        }

        //
        // GET: /ArtistSubType/Edit/5

        public ActionResult Edit(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = Db.ArtistSubTypes.Find(id);
            ViewData["ArtistTypes"] = new SelectList(Db.ArtistTypes.ToList(), "Id", "ArtistType", artistsubtypemodel.ArtistTypeId);
            return View(artistsubtypemodel);
        }

        //
        // POST: /ArtistSubType/Edit/5

        [HttpPost]
        public ActionResult Edit(ArtistSubTypeModel artistsubtypemodel)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(artistsubtypemodel).State = EntityState.Modified;

                foreach (ArtistModel artist in Db.Artists.ToList())
                {
                    if (artist.ArtistSubTypeId == artistsubtypemodel.Id && artist.ArtistTypeId != artistsubtypemodel.ArtistTypeId)
                    {
                        artist.ArtistTypeId = artistsubtypemodel.ArtistTypeId;
                        Db.Entry(artist).State = EntityState.Modified;
                    }
                }
                
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["ArtistTypes"] = new SelectList(Db.ArtistTypes.ToList(), "Id", "ArtistType", artistsubtypemodel.ArtistTypeId);
            return View(artistsubtypemodel);
        }

        //
        // GET: /ArtistSubType/Delete/5

        public ActionResult Delete(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = Db.ArtistSubTypes.Find(id);
            return View(artistsubtypemodel);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistSubTypeModel artistsubtypemodel = Db.ArtistSubTypes.Find(id);
            Db.ArtistSubTypes.Remove(artistsubtypemodel);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ContentResult AjaxGet(int id)
        {
            IEnumerable<ArtistSubTypeModel> artistSubTypes = Db.ArtistSubTypes.ToList().Where(x => x.ArtistTypeId == id);
            StringBuilder sb = new StringBuilder();

            foreach (ArtistSubTypeModel artistSubType in artistSubTypes)
            {
                sb.AppendFormat("<option value='{0}'>{1}</option>", artistSubType.Id, artistSubType.ArtistSubType);
            }

            return Content(sb.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            Db.Dispose();
            base.Dispose(disposing);
        }
    }
}
