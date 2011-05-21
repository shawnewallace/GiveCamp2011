using System.Data.Entity;
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
    }
}