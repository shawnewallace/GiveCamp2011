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
    public class SiteModelsController : ColumbusGiveCamp2011ControllerBase
    {
        //private WebContext context = new WebContext();

        //
        // GET: /SiteModels/

        public ViewResult Index()
        {
            var model = Db.SiteModels
               .Select(SM => SM)
               .OrderBy(SM => SM.Name)
               .ToList();

            return View("Index", model);
        }

        //
        // GET: /SiteModels/Details/5

        public ViewResult Details(int id)
        {
            SiteModel sitemodel = Db.SiteModels.Include("siteLinks").Single(x => x.Id == id);
            return View(sitemodel);
        }

        public PartialViewResult PublicDetails()
        {
            var model = Db.SiteModels.FirstOrDefault();

            return PartialView("_PublicDetails", model);
        }

        //
        // GET: /SiteModels/Create

        public ViewResult Create()
        {
            return View();
        } 

        //
        // POST: /SiteModels/Create

        [HttpPost]
        public ActionResult Create(SiteModel sitemodel)
        {
            if (ModelState.IsValid)
            {
                Db.SiteModels.Add(sitemodel);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(sitemodel);
        }
        
        //
        // GET: /SiteModels/Edit/5

        public ViewResult Edit(int id)
        {
            SiteModel sitemodel = Db.SiteModels.Include("siteLinks").Single(x => x.Id == id);
            return View(sitemodel);
        }

        //
        // POST: /SiteModels/Edit/5

        [HttpPost]
        public ActionResult Edit(SiteModel sitemodel)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(sitemodel).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sitemodel);
        }

        //
        // GET: /SiteModels/Delete/5
 
        public ActionResult Delete(int id)
        {
            SiteModel sitemodel = Db.SiteModels.Single(x => x.Id == id);
            return View(sitemodel);
        }

        //
        // POST: /SiteModels/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteModel sitemodel = Db.SiteModels.Single(x => x.Id == id);
            Db.SiteModels.Remove(sitemodel);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteLink(int id)
        {
            SiteLink siteLink = Db.SiteLinks.Single(x => x.Id == id);
            Db.SiteLinks.Remove(siteLink);
            Db.SaveChanges();
            return Content("");
        }

        [HttpPost]
        public ActionResult Save(SiteModel sitemodel)
        {

            foreach (var item in sitemodel.siteLinks)
            {
                if (Db.SiteLinks.Count(f => f.Id == item.Id) == 0)
                {                    
                    item.siteModel = Db.SiteModels.Single(f => f.Id == sitemodel.Id);   
                    Db.SiteLinks.Add(item);
                    Db.SaveChanges();
                }
                else
                {
                    Db.Entry(item).State = EntityState.Modified;
                    Db.SaveChanges();
                }
            }
            
            return Content("");
        
        }

    }
}