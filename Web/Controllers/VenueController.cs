using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class VenueController : ColumbusGiveCamp2011ControllerBase
    {
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

        public ViewResult Details(int id)
        {
            VenueModel model = Db.Venues.Find(id);
            if (model.Id <= 0)
            {
                return View("NoVenue");
            }
            return View(model);
        }

        //
        // GET: /Venue/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Venue/Create

        [HttpPost]
        public ActionResult Create(VenueModel model)
        {
            if (ModelState.IsValid)
            {
                Db.Venues.Add(model);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Venue/Edit/5

        public ActionResult Edit(int id)
        {
            VenueModel model = Db.Venues.Find(id);
            return View(model);
        }

        //
        // POST: /Venue/Edit/5

        [HttpPost]
        public ActionResult Edit(VenueModel model)
        {
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

        public ActionResult Delete(int id)
        {
            VenueModel model = Db.Venues.Find(id);
            return View(model);
        }

        //
        // POST: /Venue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueModel model = Db.Venues.Find(id);
            Db.Venues.Remove(model);
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
