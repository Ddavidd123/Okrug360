using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Entities;

namespace Okrug360.Content.Api.Services;

public interface INewsArticleService
{
    Task<IReadOnlyList<NewsArticle>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<NewsArticle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<NewsArticle> CreateAsync(
        CreateNewsArticleRequest request,
        CancellationToken cancellationToken);
}