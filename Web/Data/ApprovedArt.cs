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
        private const string ART_FORMAT =  "Artist_{0}_Image_{1}.{3}";
        private const string IMAGE_FILE_CAPTURE_EXPRESSION = @"Artist_(\d+)_Image_(.+)\.png";
        private const string IMAGE_FILE_EXPRESSION = "Artist_*_Image_*.png";
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

        public static void StoreCoverFlowImage(string file)
        {
            Image incoming = Bitmap.FromFile(file);
            int prefHeight =  
                (int)Math.Floor((double)(ImageService.PREFERRED_COVERFLOW_WIDTH/incoming.Width)*incoming.Height);
            Image coverFlow = ResizeImage(incoming, ImageService.PREFERRED_COVERFLOW_WIDTH, prefHeight);
            coverFlow.Save(GetFullCoverFlowPath()+Path.GetFileNameWithoutExtension(file) + ".png");
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
    }
}