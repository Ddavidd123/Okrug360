using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Entities;

using Okrug360.Content.Api.Repositories;

namespace Okrug360.Content.Api.Services;

public sealed class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _repository;

    public NewsArticleService(INewsArticleRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<NewsArticle>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return _repository.GetAllAsync(cancellationToken);
    }

    public Task<NewsArticle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<NewsArticle> CreateAsync(
        CreateNewsArticleRequest request,
        CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var article = new NewsArticle
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            Summary = request.Summary.Trim(),
            Content = request.Content.Trim(),
            CreatedAt = now,
            PublishedAt = request.PublishImmediately ? now : null,
            IsPublished = request.PublishImmediately
        };

        await _repository.AddAsync(article, cancellationToken);

        return article;
    }
}