using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ColumbusGiveCamp2011Context : DbContext
    {
        public DbSet<ArtistModel> Artists { get; set; }
    }
}