using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Entities
{
    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public PlaceCategory Category { get; set; }

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = "Vranje";

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get;set; }

        public DateTime? PublishedAt {  get; set; }

        public bool IsPublished {  get; set; }
    }
}
