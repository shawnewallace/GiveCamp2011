using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Entry<T>
    {
        public T Data { get; set; }
        public bool Enabled { get; set; }
    }
}