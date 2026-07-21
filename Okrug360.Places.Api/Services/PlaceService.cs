using Okrug360.Places.Api.Dtos;
using Okrug360.Places.Api.Entities;
using Okrug360.Places.Api.Enums;
using Okrug360.Places.Api.Mappings;
using Okrug360.Places.Api.Repositories;

namespace Okrug360.Places.Api.Services;

public sealed class PlaceService : IPlaceService
{
    private readonly IPlaceRepository _repository;

    public PlaceService(IPlaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedPlacesResponse> GetPublishedAsync(
        int page,
        int pageSize,
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        if (pageSize > 50)
        {
            pageSize = 50;
        }

        var (places, totalCount) = await _repository.GetPublishedAsync(
            page,
            pageSize,
            category,
            city,
            cancellationToken);

        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedPlacesResponse
        {
            Items = places.ToResponseList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<IReadOnlyList<PlaceMapMarkerResponse>> GetPublishedMapMarkersAsync(
        PlaceCategory? category,
        string? city,
        CancellationToken cancellationToken)
    {
        var places = await _repository.GetPublishedMapMarkersAsync(
            category,
            city,
            cancellationToken);

        return places.ToMapMarkerList();
    }

    public async Task<PlaceResponse?> GetPublishedByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _repository.GetPublishedByIdAsync(
            id,
            cancellationToken);

        return place?.ToResponse();
    }

    public async Task<PlaceResponse> CreateAsync(
        CreatePlaceRequest request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var place = new Place
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Description = request.Description.Trim(),
            Category = request.Category,
            Address = request.Address.Trim(),
            City = request.City.Trim(),
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl)
                ? null
                : request.ImageUrl.Trim(),
            CreatedAt = now,
            UpdatedAt = null,
            PublishedAt = request.PublishImmediately ? now : null,
            IsPublished = request.PublishImmediately
        };

        await _repository.AddAsync(place, cancellationToken);

        return place.ToResponse();
    }

    public async Task<PlaceResponse?> UpdateAsync(
        Guid id,
        UpdatePlaceRequest request,
        CancellationToken cancellationToken)
    {
        var place = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return null;
        }

        place.Name = request.Name.Trim();
        place.Description = request.Description.Trim();
        place.Category = request.Category;
        place.Address = request.Address.Trim();
        place.City = request.City.Trim();
        place.Latitude = request.Latitude;
        place.Longitude = request.Longitude;
        place.ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl)
            ? null
            : request.ImageUrl.Trim();
        place.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(place, cancellationToken);

        return place.ToResponse();
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return false;
        }

        await _repository.DeleteAsync(place, cancellationToken);

        return true;
    }

    public async Task<PlaceResponse?> PublishAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return null;
        }

        if (!place.IsPublished)
        {
            place.IsPublished = true;
            place.PublishedAt = DateTime.UtcNow;
            place.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(place, cancellationToken);
        }

        return place.ToResponse();
    }

    public async Task<PlaceResponse?> ArchiveAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return null;
        }

        if (place.IsPublished)
        {
            place.IsPublished = false;
            place.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(place, cancellationToken);
        }

        return place.ToResponse();
    }
}