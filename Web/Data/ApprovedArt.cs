using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;
using System.Web.Mvc;

namespace Web.Data
{
    public static class ImageService
    {
        private const string COVER_FLOW_DIRECTORY = "/Content/CoverFlow/";
        private const string COVER_FLOW_WEB_DIRECTORY = "/Content/CoverFlow/{0}";
        private const string UNAPPROVED_IMAGE_DIRECTORY = @"/Content/Unapproved";
        private const string UNAPPROVED_IMAGE_WEB_DIRECTORY = @"/Content/Unapproved/{0}";
        private const string APPROVED_IMAGE_DIRECTORY = @"/Content/Approved";
        private const string APPROVED_IMAGE_WEB_DIRECTORY = @"/Content/Approved/{0}";

        private const string ART_FORMAT =  "Artist_{0}_Image_{1}.{3}";
        private const string ARTIST_EXPRESSION = "";
        private const string IMAGE_FILE_EXPRESSION = "";
        private const byte PREFERRED_COVERFLOW_HEIGHT = 86;

        public static IEnumerable<string> GetCoverFlowArt()
        {
            return Directory.EnumerateFiles(
                HttpContext.Current.ApplicationInstance.Server.MapPath(
                HttpContext.Current.Request.ApplicationPath)+COVER_FLOW_DIRECTORY).Select(
                (filePath) => string.Format(COVER_FLOW_WEB_DIRECTORY, Path.GetFileName(filePath)));
        }

        public static IEnumerable<string> GetUnapprovedArt()
        {
            return Directory.EnumerateFiles(
                HttpContext.Current.ApplicationInstance.Server.MapPath(
                HttpContext.Current.Request.ApplicationPath) + UNAPPROVED_IMAGE_DIRECTORY).Select(
                (filePath) => string.Format(UNAPPROVED_IMAGE_WEB_DIRECTORY, Path.GetFileName(filePath)));
        }

        public static IEnumerable<string> GetApprovedArt()
        {
            return Directory.EnumerateFiles(
                HttpContext.Current.ApplicationInstance.Server.MapPath(
                HttpContext.Current.Request.ApplicationPath) + APPROVED_IMAGE_DIRECTORY).Select(
                (filePath) => string.Format(APPROVED_IMAGE_WEB_DIRECTORY, Path.GetFileName(filePath)));
        }

        public static void ApproveArt(string image)
        {
            var rawFileName = image.Replace(UNAPPROVED_IMAGE_DIRECTORY, "");
            var fullFileName = HttpContext.Current.ApplicationInstance.Server.MapPath(UNAPPROVED_IMAGE_DIRECTORY) + rawFileName;
            var targetFileName = HttpContext.Current.ApplicationInstance.Server.MapPath(APPROVED_IMAGE_DIRECTORY) + rawFileName;

            try { File.Move(fullFileName, targetFileName); }
            catch (Exception)
            {
            }
        }

        public static void RejectArt(string image)
        {
            var rawFileName = image.Replace(UNAPPROVED_IMAGE_DIRECTORY, "");
            var fullFileName = HttpContext.Current.ApplicationInstance.Server.MapPath(UNAPPROVED_IMAGE_DIRECTORY) + rawFileName;

            try { File.Delete(fullFileName); }
            catch (Exception)
            {
            }
        }
    }
}