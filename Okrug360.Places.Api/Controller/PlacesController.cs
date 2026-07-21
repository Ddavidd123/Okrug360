using Microsoft.AspNetCore.Mvc;
using Okrug360.Places.Api.Dtos;
using Okrug360.Places.Api.Enums;
using Okrug360.Places.Api.Services;

namespace Okrug360.Places.Api.Controllers;

[ApiController]
[Route("api/places")]
public sealed class PlacesController : ControllerBase
{
    private readonly IPlaceService _service;

    public PlacesController(IPlaceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PagedPlacesResponse>> GetPublished(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] PlaceCategory? category = null,
        [FromQuery] string? city = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.GetPublishedAsync(
            page,
            pageSize,
            category,
            city,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("map")]
    public async Task<ActionResult<IReadOnlyList<PlaceMapMarkerResponse>>> GetMapMarkers(
        [FromQuery] PlaceCategory? category = null,
        [FromQuery] string? city = null,
        CancellationToken cancellationToken = default)
    {
        var markers = await _service.GetPublishedMapMarkersAsync(
            category,
            city,
            cancellationToken);

        return Ok(markers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlaceResponse>> GetPublishedById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _service.GetPublishedByIdAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return NotFound();
        }

        return Ok(place);
    }

    [HttpPost]
    public async Task<ActionResult<PlaceResponse>> Create(
        CreatePlaceRequest request,
        CancellationToken cancellationToken)
    {
        var place = await _service.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetPublishedById),
            new { id = place.Id },
            place);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PlaceResponse>> Update(
        Guid id,
        UpdatePlaceRequest request,
        CancellationToken cancellationToken)
    {
        var place = await _service.UpdateAsync(
            id,
            request,
            cancellationToken);

        if (place is null)
        {
            return NotFound();
        }

        return Ok(place);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{id:guid}/publish")]
    public async Task<ActionResult<PlaceResponse>> Publish(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _service.PublishAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return NotFound();
        }

        return Ok(place);
    }

    [HttpPost("{id:guid}/archive")]
    public async Task<ActionResult<PlaceResponse>> Archive(
        Guid id,
        CancellationToken cancellationToken)
    {
        var place = await _service.ArchiveAsync(
            id,
            cancellationToken);

        if (place is null)
        {
            return NotFound();
        }

        return Ok(place);
    }
}