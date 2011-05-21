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
            var model = Db.Venues.Select(v => v).ToList();

            return View("Index", model);
        }
    }
}
