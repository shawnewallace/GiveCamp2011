namespace Web.Models
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Type { get; set; }

        public SearchResult(VenueModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Bio = string.Format("{0}, {1}, {2} {3} - {4}", model.Address, model.City, model.State, model.Zip, model.PhoneNumber);
            Type = SearchResultType.Venue.ToString();
        }

        public SearchResult(ArtistModel model)
        {
            Id = model.Id;
            Name = string.Format("{0}, {1}", model.LastName.Data, model.FirstName.Data);
            Bio = model.Biography;
            Type = SearchResultType.Artist.ToString();
        }
    }
}