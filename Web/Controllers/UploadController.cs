using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Web.Controllers
{
    public partial class UploadController : Controller
    {

        public ActionResult Save(IEnumerable<HttpPostedFileBase> pictures, int artistId)
        {
            foreach (var file in pictures)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileName = Path.GetFileName(file.FileName);
                var physicalPath = Path.Combine(Server.MapPath(Web.Data.ImageService.UnapprovedDirectory()),String.Format("{0:00000}_{1}",artistId, fileName));
                file.SaveAs(physicalPath);
                
            }            

            // Return the partial view
            return Content("");
            //return PartialView("ArtUpload",artistId);        
        }

        public ActionResult Remove(string[] fileNames, int artistId)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath(Web.Data.ImageService.UnapprovedDirectory()),String.Format("{0:00000}_",artistId), fileName);

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
    }
}
