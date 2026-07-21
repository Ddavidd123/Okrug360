using Okrug360.Places.Api.Dtos;
using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Services;

public interface IPlaceService
{
    Task<PagedPlacesResponse> GetPublishedAsync(
        int page,
        int pageSize,
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<PlaceMapMarkerResponse>> GetPublishedMapMarkersAsync(
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken);

    Task<PlaceResponse?> GetPublishedByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PlaceResponse> CreateAsync(
        CreatePlaceRequest request,
        CancellationToken cancellationToken);

    Task<PlaceResponse?> UpdateAsync(
        Guid id,
        UpdatePlaceRequest request,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PlaceResponse?> PublishAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PlaceResponse?> ArchiveAsync(
        Guid id,
        CancellationToken cancellationToken);
}