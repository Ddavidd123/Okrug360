using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Dtos;

public sealed class PlaceMapMarkerResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public PlaceCategory Category { get; init; }

    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public string? ImageUrl { get; init; }
}