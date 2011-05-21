using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class VenueController : Controller
    {
        public IColumbusGiveCamp2011Context db
        {
            get { return _db ?? (_db = new ColumbusGiveCamp2011Context()); }
            set { _db = value; }
        }
        private IColumbusGiveCamp2011Context _db;

        public ViewResult Index()
        {
            var model = db.Venues.Select(v => v).ToList();

            return View("Index", model);
        }
    }
}
