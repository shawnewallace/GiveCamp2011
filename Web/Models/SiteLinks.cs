using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SiteLink
    {
        public int Id { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual  SiteModel siteModel { get; set; }
        
    }
}
