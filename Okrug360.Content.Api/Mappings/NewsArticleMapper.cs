using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Entities;

namespace Okrug360.Content.Api.Mappings;

public static class NewsArticleMapper
{
    public static NewsArticleResponse ToResponse(this NewsArticle article)
    {
        return new NewsArticleResponse
        {
            Id = article.Id,
            Title = article.Title,
            Summary = article.Summary,
            Content = article.Content,
            CreatedAt = article.CreatedAt,
            PublishedAt = article.PublishedAt,
            IsPublished = article.IsPublished
        };
    }

    public static IReadOnlyList<NewsArticleResponse> ToResponseList(
        this IEnumerable<NewsArticle> articles)
    {
        return articles.Select(ToResponse).ToList();
    }
}
