using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Dtos;

public sealed class PlaceResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public PlaceCategory Category { get; init; }

    public string Address { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public string? ImageUrl { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public DateTime? PublishedAt { get; init; }

    public bool IsPublished { get; init; }
}