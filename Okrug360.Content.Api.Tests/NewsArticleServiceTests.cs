using Moq;
using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Entities;
using Okrug360.Content.Api.Repositories;
using Okrug360.Content.Api.Services;

namespace Okrug360.Content.Api.Tests;

public sealed class NewsArticleServiceTests
{
    [Fact]
    public async Task CreateAsync_WhenPublishImmediatelyIsTrue_SetsPublishedFields()
    {
        // Arrange — priprema
        var repositoryMock = new Mock<INewsArticleRepository>();

        NewsArticle? savedArticle = null;

        repositoryMock
            .Setup(x => x.AddAsync(
                It.IsAny<NewsArticle>(),
                It.IsAny<CancellationToken>()))
            .Callback<NewsArticle, CancellationToken>((article, _) =>
            {
                savedArticle = article;
            })
            .Returns(Task.CompletedTask);

        var service = new NewsArticleService(repositoryMock.Object);

        var request = new CreateNewsArticleRequest
        {
            Title = "  Test vest  ",
            Summary = "  Kratak opis  ",
            Content = "  Pun tekst  ",
            PublishImmediately = true
        };

        // Act — izvršavanje
        var result = await service.CreateAsync(
            request,
            CancellationToken.None);

        // Assert — provera
        Assert.NotNull(result);
        Assert.Equal("Test vest", result.Title);
        Assert.Equal("Kratak opis", result.Summary);
        Assert.Equal("Pun tekst", result.Content);
        Assert.True(result.IsPublished);
        Assert.NotNull(result.PublishedAt);

        Assert.NotNull(savedArticle);
        Assert.True(savedArticle.IsPublished);
        Assert.NotNull(savedArticle.PublishedAt);

        repositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<NewsArticle>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}