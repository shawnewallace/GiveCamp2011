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
            FirstName = new TextEntry();
            LastName = new TextEntry();
            Address1 = new TextEntry();
            Address2 = new TextEntry();
            City = new TextEntry();
            State = new TextEntry();
            Zip = new TextEntry();
            Phone = new TextEntry();
            CellPhone = new TextEntry();
            Email = new TextEntry();
            Dob = new Birthday();
            Website = new TextEntry();
            Twitter = new TextEntry();
            Facebook = new TextEntry();
        }

        public int Id { get; set; }
        
        public TextEntry FirstName { get; set; }
        public TextEntry LastName { get; set; }
        public TextEntry Address1 { get; set; }
        public TextEntry Address2 { get; set; }
        public TextEntry City { get; set; }
        public TextEntry State { get; set; }
        public TextEntry Zip { get; set; }
        public TextEntry Phone { get; set; }
        public TextEntry CellPhone { get; set; }
        public TextEntry Email { get; set; }
        public Birthday Dob { get; set; }

        public TextEntry Website { get; set; }
        public TextEntry Twitter { get; set; }
        public TextEntry Facebook { get; set; }

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