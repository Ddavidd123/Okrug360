using Microsoft.EntityFrameworkCore;
using Okrug360.Places.Api.Data;
using Okrug360.Places.Api.Entities;
using Okrug360.Places.Api.Enums;

namespace Okrug360.Places.Api.Repositories;

public sealed class PlaceRepository : IPlaceRepository
{
    private readonly PlacesDbContext _dbContext;

    public PlaceRepository(PlacesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(IReadOnlyList<Place> Items, int TotalCount)> GetPublishedAsync(
        int page,
        int pageSize,
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken)
    {
        var query = BuildPublishedQuery(category, city);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<IReadOnlyList<Place>> GetPublishedMapMarkersAsync(
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken)
    {
        return await BuildPublishedQuery(category, city)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Place?> GetPublishedByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Places
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id && x.IsPublished,
                cancellationToken);
    }

    public async Task<Place?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Places
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Place place,
        CancellationToken cancellationToken)
    {
        _dbContext.Places.Add(place);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Place place,
        CancellationToken cancellationToken)
    {
        _dbContext.Places.Update(place);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        Place place,
        CancellationToken cancellationToken)
    {
        _dbContext.Places.Remove(place);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<Place> BuildPublishedQuery(
        PlaceCategory? category,
        string? city)
    {
        var query = _dbContext.Places
            .AsNoTracking()
            .Where(x => x.IsPublished);

        if (category.HasValue)
        {
            query = query.Where(x => x.Category == category.Value);
        }

        if (!string.IsNullOrWhiteSpace(city))
        {
            var normalizedCity = city.Trim();
            query = query.Where(x => x.City == normalizedCity);
        }

        return query;
    }
}