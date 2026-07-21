namespace Okrug360.Places.Api.Dtos
{
    public sealed class PagedPlacesResponse
    {
        public IReadOnlyList<PlaceResponse> Items { get; init; } = Array.Empty<PlaceResponse>();
        public int Page { get; init; }

        public int PageSize { get; init; }
        public int TotalCount { get; init; }

        public int TotalPages { get; init; }
    }
}
