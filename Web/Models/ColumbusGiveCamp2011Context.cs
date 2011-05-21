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
		IDbSet<ArtistTypeModel> ArtistTypes { get; set; }
		IDbSet<ArtistSubTypeModel> ArtistSubTypes { get; set; }
        IDbSet<SiteModel> SiteModels { get; set; }
        IDbSet<SiteLink> SiteLinks { get; set; } 
		int SaveChanges();
		DbEntityEntry Entry(object entity);
		void Dispose();
	}

	public class ColumbusGiveCamp2011Context : DbContext, IColumbusGiveCamp2011Context
	{
		public IDbSet<ArtistModel> Artists { get; set; }
		public IDbSet<VenueModel> Venues { get; set; }
		public IDbSet<ArtistTypeModel> ArtistTypes { get; set; }
		public IDbSet<ArtistSubTypeModel> ArtistSubTypes { get; set; }
        public IDbSet<SiteModel> SiteModels { get; set; }
        public IDbSet<SiteLink> SiteLinks { get; set; } 
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
		}
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
            var siteModels = new List<SiteModel>
							 {
								 new SiteModel
									 {
										 Name = "Gallery 202 Co-op",
										 Address = "38 North State St",
										 City = "Westerville",
										 State = "OH",
										 Zip = "43081",
										 PhoneNumber = "614.890.8202", 
                                          Hours = "Wed. 12-6pm, Thurs/Fri 12-4pm, Sat. 11-3 pm",
                                           siteLinks = new List<SiteLink>() {
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwannual.html", Name = "Annual Community Events" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwbed.html" , Name = "Bed & Breakfasts, Community Sites" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwgallery.html" , Name = "Galleries, Art Leagues, Visual Art Centers" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwlandmarks.html" , Name = "Landmarks, Public Art, Sculptures, and Murals" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwmusic.html" , Name = "Music, Dance, Theatre" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwparks.html" , Name = "Parks, Museums" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwsports.html" , Name = "Sports, Athletics, Kids" },
                                               new SiteLink() { Destination = "http://www.gallery202online.com/linkpages/vwtour.html" , Name = "Virtual Tours" },
                                           }
									 }
							 };
            siteModels.ForEach(d => context.SiteModels.Add(d));

		}
	}
}
