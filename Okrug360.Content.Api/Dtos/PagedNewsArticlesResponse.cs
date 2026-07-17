namespace Okrug360.Content.Api.Dtos
{
    public sealed class PagedNewsArticlesResponse
    {
        public IReadOnlyList<NewsArticleResponse> Items { get; init; } =
            Array.Empty<NewsArticleResponse>();

        public int Page {  get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }    

        public int TotalPages { get; init; }
    }
}
