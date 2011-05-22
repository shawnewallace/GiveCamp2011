using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Web.Helpers;

namespace Web.Models
{
    public class VenueModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string UserId { get; set; }

        [RegularExpression(Constants.REGEX_ZIP_CODE)]
        public string Zip { get; set; }

        [RegularExpression(Constants.REGEX_PHONE_NUMBER)]
        public string PhoneNumber { get; set; }
    }
}