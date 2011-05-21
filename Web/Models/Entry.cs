using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    [Complex]
    public class TextEntry
    {
        public string Data { get; set; }
        public bool Enabled { get; set; }
    }

    //[Complex]
    //public class UriEntry
    //{
    //    public Uri Data { get; set; }
    //    public bool Enabled { get; set; }
    //}

    [Complex]
    public class DateEntry
    {
        public DateTime Data { get; set; }
        public bool Enabled { get; set; }
    }
}