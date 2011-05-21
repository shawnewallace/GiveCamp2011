using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ColumbusGiveCamp2011ControllerBase : Controller
    {
        public IColumbusGiveCamp2011Context Db
        {
            get { return _db ?? (_db = new ColumbusGiveCamp2011Context()); }
            set { _db = value; }
        }
        private IColumbusGiveCamp2011Context _db;
    }
}