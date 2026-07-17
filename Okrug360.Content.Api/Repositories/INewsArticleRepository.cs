using Okrug360.Content.Api.Entities;

namespace Okrug360.Content.Api.Repositories;

public interface INewsArticleRepository
{
    Task<IReadOnlyList<NewsArticle>> GetPublishedAsync(
        CancellationToken cancellationToken);

    Task<NewsArticle?> GetPublishedByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task AddAsync(
        NewsArticle article,
        CancellationToken cancellationToken);
}