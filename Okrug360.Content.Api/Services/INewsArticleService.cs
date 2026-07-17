using Okrug360.Content.Api.Dtos;

namespace Okrug360.Content.Api.Services;

public interface INewsArticleService
{
    Task<IReadOnlyList<NewsArticleResponse>> GetPublishedAsync(
        CancellationToken cancellationToken);

    Task<PagedNewsArticlesResponse> GetPublishedAsync(
    int page,
    int pageSize,
    CancellationToken cancellationToken);

    Task<NewsArticleResponse> CreateAsync(
        CreateNewsArticleRequest request,
        CancellationToken cancellationToken);

    Task<NewsArticleResponse?> UpdateAsync(
       Guid id,
       UpdateNewsArticleRequest request,
       CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<NewsArticleResponse?> PublishAsync(
    Guid id,
    CancellationToken cancellationToken);

    Task<NewsArticleResponse?> ArchiveAsync(
        Guid id,
        CancellationToken cancellationToken);
}