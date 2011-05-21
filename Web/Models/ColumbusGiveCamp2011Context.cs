using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public interface IColumbusGiveCamp2011Context
    {
        IDbSet<ArtistModel> Artists { get; set; }
        IDbSet<VenueModel> Venues { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
        void Dispose();
    }

    public class ColumbusGiveCamp2011Context : DbContext, IColumbusGiveCamp2011Context
    {
        public IDbSet<ArtistModel> Artists { get; set; }
        public IDbSet<VenueModel> Venues { get; set; }
    }

    public class ColumbusGiveCamp2011Initializer : DropCreateDatabaseIfModelChanges<ColumbusGiveCamp2011Context>
    {
        protected override void Seed(ColumbusGiveCamp2011Context context)
        {
            var venues = new List<VenueModel>
                             {
                                 new VenueModel
                                     {
                                         Name = "First Venue",
                                         Address = "123 My Street",
                                         City = "Westerville",
                                         State = "OH",
                                         Zip = "12345",
                                         PhoneNumber = "123.456.7890"
                                     }
                             };
            venues.ForEach(d => context.Venues.Add(d));
        }
    }
}