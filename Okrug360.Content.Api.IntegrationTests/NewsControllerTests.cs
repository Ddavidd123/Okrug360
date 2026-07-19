using System.Net;
using System.Net.Http.Json;
using Okrug360.Content.Api.Dtos;

namespace Okrug360.Content.Api.IntegrationTests;

public sealed class NewsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public NewsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPublished_ReturnsOk()
    {
        var response = await _client.GetAsync(
            "/api/news?page=1&pageSize=10");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result =
            await response.Content.ReadFromJsonAsync<PagedNewsArticlesResponse>();

        Assert.NotNull(result);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Fact]
    public async Task Create_WithValidRequest_ReturnsCreated()
    {
        var request = CreateRequest(publishImmediately: true);

        var response = await _client.PostAsJsonAsync(
            "/api/news",
            request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var article =
            await response.Content.ReadFromJsonAsync<NewsArticleResponse>();

        Assert.NotNull(article);
        Assert.Equal(request.Title, article.Title);
        Assert.True(article.IsPublished);
        Assert.NotEqual(Guid.Empty, article.Id);
    }

    [Fact]
    public async Task Create_WithInvalidRequest_ReturnsBadRequest()
    {
        var request = new CreateNewsArticleRequest
        {
            Title = "",
            Summary = "",
            Content = "",
            PublishImmediately = false
        };

        var response = await _client.PostAsJsonAsync(
            "/api/news",
            request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WhenArticleIsDraft_ReturnsNotFound()
    {
        var article = await CreateArticleAsync(
            publishImmediately: false);

        var response = await _client.GetAsync(
            $"/api/news/{article.Id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WhenArticleIsPublished_ReturnsOk()
    {
        var article = await CreateArticleAsync(
            publishImmediately: true);

        var response = await _client.GetAsync(
            $"/api/news/{article.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result =
            await response.Content.ReadFromJsonAsync<NewsArticleResponse>();

        Assert.NotNull(result);
        Assert.Equal(article.Id, result.Id);
    }

    [Fact]
    public async Task Update_WhenArticleExists_UpdatesArticle()
    {
        var article = await CreateArticleAsync(
            publishImmediately: true);

        var request = new UpdateNewsArticleRequest
        {
            Title = "Izmenjen naslov",
            Summary = "Izmenjen opis",
            Content = "Izmenjen sadržaj"
        };

        var response = await _client.PutAsJsonAsync(
            $"/api/news/{article.Id}",
            request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result =
            await response.Content.ReadFromJsonAsync<NewsArticleResponse>();

        Assert.NotNull(result);
        Assert.Equal("Izmenjen naslov", result.Title);
        Assert.Equal("Izmenjen opis", result.Summary);
        Assert.Equal("Izmenjen sadržaj", result.Content);
    }

    [Fact]
    public async Task PublishAndArchive_ChangesPublicVisibility()
    {
        var article = await CreateArticleAsync(
            publishImmediately: false);

        var publishResponse = await _client.PostAsync(
            $"/api/news/{article.Id}/publish",
            content: null);

        Assert.Equal(HttpStatusCode.OK, publishResponse.StatusCode);

        var publicResponse = await _client.GetAsync(
            $"/api/news/{article.Id}");

        Assert.Equal(HttpStatusCode.OK, publicResponse.StatusCode);

        var archiveResponse = await _client.PostAsync(
            $"/api/news/{article.Id}/archive",
            content: null);

        Assert.Equal(HttpStatusCode.OK, archiveResponse.StatusCode);

        var archivedResponse = await _client.GetAsync(
            $"/api/news/{article.Id}");

        Assert.Equal(HttpStatusCode.NotFound, archivedResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_WhenArticleExists_ReturnsNoContent()
    {
        var article = await CreateArticleAsync(
            publishImmediately: true);

        var deleteResponse = await _client.DeleteAsync(
            $"/api/news/{article.Id}");

        Assert.Equal(
            HttpStatusCode.NoContent,
            deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync(
            $"/api/news/{article.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    private async Task<NewsArticleResponse> CreateArticleAsync(
        bool publishImmediately)
    {
        var request = CreateRequest(publishImmediately);

        var response = await _client.PostAsJsonAsync(
            "/api/news",
            request);

        response.EnsureSuccessStatusCode();

        var article =
            await response.Content.ReadFromJsonAsync<NewsArticleResponse>();

        Assert.NotNull(article);

        return article;
    }

    private static CreateNewsArticleRequest CreateRequest(
        bool publishImmediately)
    {
        var uniqueValue = Guid.NewGuid().ToString("N");

        return new CreateNewsArticleRequest
        {
            Title = $"Test vest {uniqueValue}",
            Summary = "Kratak opis test vesti",
            Content = "Sadržaj integration testa",
            PublishImmediately = publishImmediately
        };
    }
}