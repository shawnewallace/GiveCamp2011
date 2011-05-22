using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Helpers;

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

    public class Birthday
    {
        public int Day { get; set; }
        public Month Month { get; set; }
        public bool Enabled { get; set; }

        public Birthday()
        {
            Day = 1;
            Month = Month.January;
        }
    }

    public enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}