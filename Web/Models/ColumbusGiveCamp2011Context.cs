﻿using System;
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
}