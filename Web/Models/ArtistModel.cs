using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArtistModel
    {
        [Column] public int Id { get; set; }
        [Column] public string FirstName { get; set; }
        [Column] public int? ArtistTypeId { get; set; }
        [Column] public int? ArtistSubTypeId { get; set; }
        [Column] public string OtherNotes { get; set; }

        internal EntityRef<ArtistTypeModel> _artistType;
        internal EntityRef<ArtistSubTypeModel> _artistSubType;

        [Association(ThisKey = "ArtistTypeId", OtherKey = "Id", Storage = "_artistType")]
        public ArtistTypeModel ArtistType
        {
            get { return _artistType.Entity; }
            internal set { _artistType.Entity = value; ArtistTypeId = value.Id; }
        }

        [Association(ThisKey = "ArtistSubTypeId", OtherKey = "Id", Storage = "_artistSubTypeId")]
        public ArtistSubTypeModel ArtistSubType
        {
            get { return _artistSubType.Entity; }
            internal set { _artistSubType.Entity = value; ArtistSubTypeId = value.Id; }
        }
    }
}
