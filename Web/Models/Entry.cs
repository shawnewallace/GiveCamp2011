using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    //public class Entry<TData>
    //{
    //    public TData Data { get; set; }
    //    public bool Enabled { get; set; }
    //}

    public class TextEntry
    {
        public string Data { get; set; }
        public bool Enabled { get; set; }
    }

    public class UriEntry
    {
        public Uri Data { get; set; }
        public bool Enabled { get; set; }
    }

    public class DateEntry
    {
        public DateTime Data { get; set; }
        public bool Enabled { get; set; }
    }
}