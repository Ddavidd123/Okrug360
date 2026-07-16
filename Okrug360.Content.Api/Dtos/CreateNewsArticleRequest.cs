namespace Okrug360.Content.Api.Dtos;

public sealed class CreateNewsArticleRequest
{
    public string Title { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool PublishImmediately { get; set; }
}