using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArtistModel
    {
        public ArtistModel()
        {
            FirstName = new Entry<string>();
            LastName = new Entry<string>();
            Address1 = new Entry<string>();
            Address2 = new Entry<string>();
            City = new Entry<string>();
            State = new Entry<string>();
            Zip = new Entry<string>();
            Phone = new Entry<string>();
            CellPhone = new Entry<string>();
            Email = new Entry<string>();
            Dob = new Entry<DateTime>();
            Website = new Entry<string>();
            Twitter = new Entry<string>();
            Facebook = new Entry<string>();
        }

        public int Id { get; set; }
        
        public Entry<string> FirstName { get; set; }
        public Entry<string> LastName { get; set; }
        public Entry<string> Address1 { get; set; }
        public Entry<string> Address2 { get; set; }
        public Entry<string> City { get; set; }
        public Entry<string> State { get; set; }
        public Entry<string> Zip { get; set; }
        public Entry<string> Phone { get; set; }
        public Entry<string> CellPhone { get; set; }
        public Entry<string> Email { get; set; }
        public Entry<DateTime> Dob { get; set; }

        public Entry<string> Website { get; set; }
        public Entry<string> Twitter { get; set; }
        public Entry<string> Facebook { get; set; }

        public bool ResidentOfWesterville { get; set; }
        public bool WorkInWesterville { get; set; }
        public bool BelongToGroups { get; set; }
        public ICollection<string> Groups { get; set; }
        public ICollection<string> Awards { get; set; }
        public bool AttendSchoolInWesterville { get; set; }
        public ICollection<School> Schools { get; set; }

        public bool ImageProvided { get; set; }

        public string Comments { get; set; }
    }

}