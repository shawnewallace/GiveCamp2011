using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Web.Data
{
    public static class ImageService
    {
        private const string APPROVED_DIRECTORY = "/Content/Approved/";
        private const string COVER_FLOW_DIRECTORY = "\\Content\\CoverFlow\\";
        private const string COVER_FLOW_WEB_DIRECTORY = "/Content/CoverFlow/{0}";
        private const string UNAPPROVED_IMAGE_DIRECTORY = @"/Content/Unapproved";
        private const string UNAPPROVED_IMAGE_WEB_DIRECTORY = @"/Content/Unapproved/{0}";
        private const string APPROVED_IMAGE_DIRECTORY = @"/Content/Approved";
        private const string APPROVED_IMAGE_WEB_DIRECTORY = @"/Content/Approved/{0}";

        private const string ART_FORMAT =  "Artist_{0}_Image_{1}.{3}";
        private const string IMAGE_FILE_CAPTURE_EXPRESSION = @"Artist_(\d+)_Image_(.+)\.png";
        private const string IMAGE_FILE_EXPRESSION = "*png";
        private const byte PREFERRED_COVERFLOW_WIDTH = 86;

        //private static Dictionary<string, List<string>> _coverFlowCache;

        private static string GetFullCoverFlowPath()
        {
            return HttpContext.Current.ApplicationInstance.Server.MapPath(
                HttpContext.Current.Request.ApplicationPath) + COVER_FLOW_DIRECTORY;
        }

        private static IEnumerable<string> GetCoverFlowFiles()
        {
            return Directory.EnumerateFiles(
                GetFullCoverFlowPath(), 
                IMAGE_FILE_EXPRESSION,
                SearchOption.TopDirectoryOnly);
        }

        public static IEnumerable<string> GetCoverFlowArt()
        {
            return GetCoverFlowFiles().Select(
                (filePath) => string.Format(COVER_FLOW_WEB_DIRECTORY, Path.GetFileName(filePath)));
        }

        public static Image GetResizedImage(string file)
        {
            Image incoming = Bitmap.FromFile(file);
            int prefHeight =
                (int)Math.Floor((ImageService.PREFERRED_COVERFLOW_WIDTH / (double)incoming.Width) * incoming.Height);
            return ResizeImage(incoming, ImageService.PREFERRED_COVERFLOW_WIDTH, prefHeight);
        }


        public static void StoreCoverFlowImage(string file)
        {
            GetResizedImage(file).Save(GetFullCoverFlowPath()+Path.GetFileNameWithoutExtension(file) + ".png");
        }

        //public static void PrepareCoverFlowCache()
        //{
        //    Dictionary<string, List<string>> newCache = new Dictionary<string, List<string>>();
        //    foreach (var fileName in GetCoverFlowFiles())
        //    {
        //        Regex r = new Regex(fileName);
        //        Match m = r.Match(ImageService.IMAGE_FILE_CAPTURE_EXPRESSION);
        //    }
        //}

        private static Image ResizeImage(Image fullSizeImage, int thumbWidth, int thumbHeight)
        {
            Image.GetThumbnailImageAbort dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(()=>false);
            return fullSizeImage.GetThumbnailImage(thumbWidth, thumbHeight, dummyCallBack, System.IntPtr.Zero);
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