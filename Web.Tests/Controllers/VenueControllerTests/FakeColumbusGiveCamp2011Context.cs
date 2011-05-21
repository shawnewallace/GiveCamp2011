using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Web.Models;

namespace Web.UI.Tests.Controllers.VenueControllerTests
{
    public class FakeColumbusGiveCamp2011Context : IColumbusGiveCamp2011Context
    {
        public FakeColumbusGiveCamp2011Context()
        {
            this.Venues = new FakeVenueSet();
        }

        public IDbSet<ArtistModel> Artists { get; set; }
        public IDbSet<VenueModel> Venues { get; set; }
        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}