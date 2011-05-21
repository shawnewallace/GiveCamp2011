using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArtistModel
    {
        public int Id { get; set; }
        
        public Entry<string> FirstName { get; set; }
        public Entry<string> LastName { get; set; }
        public Entry<string> Address1 { get; set; }
        public Entry<string> Address2 { get; set; }
        public Entry<string> City { get; set; }
        public Entry<string> State { get; set; }
        public Entry<string> Zip { get; set; }
        public Entry<string> CellPhone { get; set; }
        public Entry<string> Email { get; set; }
        public Entry<DateTime> Dob { get; set; }
        
        public Entry<Uri> Website { get; set; }
        public Entry<string> Twitter { get; set; }
        public Entry<string> Facebook { get; set; }
        
        public Entry<bool> ResidentOfWesterville { get; set; }
        public Entry<bool> WorkInWesterville { get; set; }
        public Entry<bool> BelongToGroups { get; set; }
        public IQueryable<string> Groups { get; set; }
        public IQueryable<string> Awards { get; set; }
        public Entry<bool> AttendSchoolInWesterville { get; set; }
        public IQueryable<School> Schools { get; set; }

        public Entry<bool> ImageProvided { get; set; }

        public Entry<string> Comments { get; set; }
    }

}