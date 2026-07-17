using Microsoft.AspNetCore.Mvc;
using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Services;

namespace Okrug360.Content.Api.Controllers;

[ApiController]
[Route("api/news")]
public sealed class NewsController : ControllerBase
{
    private readonly INewsArticleService _service;

    public NewsController(INewsArticleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NewsArticleResponse>>> GetPublished(
        CancellationToken cancellationToken)
    {
        var articles = await _service.GetPublishedAsync(cancellationToken);

        return Ok(articles);
    }

    [HttpGet]
    public async Task<ActionResult<PagedNewsArticlesResponse>> GetPublished(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    CancellationToken cancellationToken = default)
    {
        var result = await _service.GetPublishedAsync(
            page,
            pageSize,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<NewsArticleResponse>> Create(
        CreateNewsArticleRequest request,
        CancellationToken cancellationToken)
    {
        var article = await _service.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetPublishedById),
            new { id = article.Id },
            article);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<NewsArticleResponse>> Update(
    Guid id,
    UpdateNewsArticleRequest request,
    CancellationToken cancellationToken)
    {
        var article = await _service.UpdateAsync(
            id,
            request,
            cancellationToken);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(article);
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
    public async Task<ActionResult<NewsArticleResponse>> Publish(
    Guid id,
    CancellationToken cancellationToken)
    {
        var article = await _service.PublishAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    [HttpPost("{id:guid}/archive")]
    public async Task<ActionResult<NewsArticleResponse>> Archive(
    Guid id,
    CancellationToken cancellationToken)
    {
        var article = await _service.ArchiveAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(article);
    }
}