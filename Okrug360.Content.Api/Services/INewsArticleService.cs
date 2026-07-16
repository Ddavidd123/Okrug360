using Okrug360.Content.Api.Dtos;

namespace Okrug360.Content.Api.Services;

public interface INewsArticleService
{
    Task<IReadOnlyList<NewsArticleResponse>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<NewsArticleResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<NewsArticleResponse> CreateAsync(
        CreateNewsArticleRequest request,
        CancellationToken cancellationToken);
}