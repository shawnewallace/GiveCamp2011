using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArtistModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public Uri Website { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
    }
}