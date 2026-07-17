using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Entities;
using Okrug360.Content.Api.Mappings;
using Okrug360.Content.Api.Repositories;

namespace Okrug360.Content.Api.Services;

public sealed class NewsArticleService : INewsArticleService
{
    private readonly INewsArticleRepository _repository;

    public NewsArticleService(INewsArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<NewsArticleResponse>> GetPublishedAsync(
        CancellationToken cancellationToken)
    {
        var articles = await _repository.GetPublishedAsync(cancellationToken);

        return articles.ToResponseList();
    }

    public async Task<PagedNewsArticlesResponse> GetPublishedAsync(
     int page,
     int pageSize,
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

        var (articles, totalCount) = await _repository.GetPublishedAsync(
            page,
            pageSize,
            cancellationToken);

        var totalPages = totalCount == 0
            ? 0
            : (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedNewsArticlesResponse
        {
            Items = articles.ToResponseList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<NewsArticleResponse> CreateAsync(
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

        return article.ToResponse();
    }

    public async Task<NewsArticleResponse?> UpdateAsync(
    Guid id,
    UpdateNewsArticleRequest request,
    CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return null;
        }

        article.Title = request.Title.Trim();
        article.Summary = request.Summary.Trim();
        article.Content = request.Content.Trim();

        await _repository.UpdateAsync(article, cancellationToken);

        return article.ToResponse();
    }

    public async Task<bool> DeleteAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return false;
        }

        await _repository.DeleteAsync(article, cancellationToken);

        return true;
    }

    public async Task<NewsArticleResponse?> PublishAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return null;

        }

        if(!article.IsPublished)
        {
            article.IsPublished = true;
            article.PublishedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(article, cancellationToken);

        }
        return article.ToResponse();
    }
    public async Task<NewsArticleResponse?> ArchiveAsync(
      Guid id,
      CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (article is null)
        {
            return null;
        }

        if (article.IsPublished)
        {
            article.IsPublished = false;
            await _repository.UpdateAsync(article, cancellationToken);
        }

        return article.ToResponse();
    }

}