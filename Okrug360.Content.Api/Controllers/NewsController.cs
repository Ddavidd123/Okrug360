using Microsoft.AspNetCore.Mvc;
using Okrug360.Content.Api.Dtos;

using Okrug360.Content.Api.Entities;
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
    public async Task<ActionResult<IReadOnlyList<NewsArticle>>> GetAll(
        CancellationToken cancellationToken)
    {
        var articles = await _service.GetAllAsync(cancellationToken);

        return Ok(articles);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NewsArticle>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var article = await _service.GetByIdAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<NewsArticle>> Create(
        CreateNewsArticleRequest request,
        CancellationToken cancellationToken)
    {
        var article = await _service.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = article.Id },
            article);
    }
}