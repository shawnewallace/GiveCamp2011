using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArtistTypeModel
    {
        public int Id { get; set; }
        public string ArtistType { get; set; }

        public List<ArtistSubTypeModel> ArtistSubType { get; set; }
    }
}
