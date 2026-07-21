using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Dtos;

public sealed class UpdatePlaceRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public PlaceCategory Category { get; set; }

    public string Address { get; set; } = string.Empty;

    public string City { get; set; } = "Vranje";

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string? ImageUrl { get; set; }
}