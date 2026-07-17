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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NewsArticleResponse>> GetPublishedById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var article = await _service.GetPublishedByIdAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(article);
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
}