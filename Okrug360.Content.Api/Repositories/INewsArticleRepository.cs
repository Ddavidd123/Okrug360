using Okrug360.Content.Api.Entities;

namespace Okrug360.Content.Api.Repositories;

public interface INewsArticleRepository
{
    Task<IReadOnlyList<NewsArticle>> GetPublishedAsync(
        CancellationToken cancellationToken);

    Task<(IReadOnlyList<NewsArticle> Items, int TotalCount)> GetPublishedAsync(
    int page,
    int pageSize,
    CancellationToken cancellationToken);

    Task<NewsArticle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task AddAsync(
        NewsArticle article,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        NewsArticle article,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        NewsArticle article,
        CancellationToken cancellationToken);
}