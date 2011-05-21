using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class School
    {
        [Key]
        public string Name { get; set; }
        public DateTime Graduation { get; set; }
    }
}
