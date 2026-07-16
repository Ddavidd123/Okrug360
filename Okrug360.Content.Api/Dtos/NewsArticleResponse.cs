namespace Okrug360.Content.Api.Dtos;

public sealed class NewsArticleResponse
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Summary { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public DateTime CreatedAt { get; init; }

    public DateTime? PublishedAt { get; init; }

    public bool IsPublished { get; init; }
}
