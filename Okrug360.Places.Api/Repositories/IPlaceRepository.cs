using Okrug360.Places.Api.Entities;
using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Repositories;

public interface IPlaceRepository
{
    Task<(IReadOnlyList<Place> Items, int TotalCount)> GetPublishedAsync(
        int page,
        int pageSize,
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Place>> GetPublishedMapMarkersAsync(
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken);

    Task<Place?> GetPublishedByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Place?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task AddAsync(
        Place place,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Place place,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        Place place,
        CancellationToken cancellationToken);
}