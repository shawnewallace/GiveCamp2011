using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class ArtistSubTypeModel
    {
        [Column] public int Id { get; set; }
        [Column] public string ArtistSubType { get; set; }
        [Column] public int ArtistTypeId { get; set; }

        internal EntityRef<ArtistTypeModel> _artistType;
        
        [Association(ThisKey = "ArtistTypeId", OtherKey="Id", Storage = "_artistType")]
        public ArtistTypeModel ArtistType
        {
            get { return _artistType.Entity; }
            internal set { _artistType.Entity = value; ArtistTypeId = value.Id; }
        }
    }
}
