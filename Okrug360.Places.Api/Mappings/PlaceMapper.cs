using Okrug360.Places.Api.Dtos;
using Okrug360.Places.Api.Entities;

namespace Okrug360.Places.Api.Mappings
{
    public static class PlaceMapper
    {
        public static PlaceResponse ToResponse(this Place place)
        {
            return new PlaceResponse
            {
                Id = place.Id,
                Name = place.Name,
                Description = place.Description,
                Category = place.Category,
                Address = place.Address,
                City = place.City,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                ImageUrl = place.ImageUrl,
                CreatedAt = place.CreatedAt,
                UpdatedAt = place.UpdatedAt,
                PublishedAt = place.PublishedAt,
                IsPublished = place.IsPublished
            };
        }
        public static PlaceMapMarkerResponse ToMapMarker(this Place place)
        {
            return new PlaceMapMarkerResponse
            {
                Id = place.Id,
                Name = place.Name,
                Category = place.Category,
                Latitude = place.Latitude,
                Longitude = place.Longitude
            };
        }

        public static IReadOnlyList<PlaceResponse> ToResponseList(
        this IEnumerable<Place> places)
        {
            return places.Select(ToResponse).ToList();
        }

        public static IReadOnlyList<PlaceMapMarkerResponse> ToMapMarkerList(
        this IEnumerable<Place> places)
        {
            return places.Select(ToMapMarker).ToList();
        }
    }
}
