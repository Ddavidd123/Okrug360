using Moq;
using Okrug360.Content.Api.Dtos;
using Okrug360.Content.Api.Entities;
using Okrug360.Content.Api.Repositories;
using Okrug360.Content.Api.Services;

namespace Okrug360.Content.Api.Tests;

public sealed class NewsArticleServiceTests
{
    private static NewsArticle NewArticle(bool isPublished)
    {
        return new NewsArticle
        {
            Id = Guid.NewGuid(),
            Title = "Naslov",
            Summary = "Opis",
            Content = "Sadrzaj",
            CreatedAt = DateTime.UtcNow,
            PublishedAt = isPublished ? DateTime.UtcNow : null,
            IsPublished = isPublished
        };
    }

    // ---------- CreateAsync ----------

    [Fact]
    public async Task CreateAsync_WhenPublishImmediatelyTrue_SetsPublishedFields()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        var service = new NewsArticleService(repositoryMock.Object);

        var request = new CreateNewsArticleRequest
        {
            Title = "  Test vest  ",
            Summary = "  Kratak opis  ",
            Content = "  Pun tekst  ",
            PublishImmediately = true
        };

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.Equal("Test vest", result.Title);
        Assert.Equal("Kratak opis", result.Summary);
        Assert.Equal("Pun tekst", result.Content);
        Assert.True(result.IsPublished);
        Assert.NotNull(result.PublishedAt);

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<NewsArticle>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenPublishImmediatelyFalse_LeavesAsDraft()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        var service = new NewsArticleService(repositoryMock.Object);

        var request = new CreateNewsArticleRequest
        {
            Title = "Draft",
            Summary = "Opis",
            Content = "Tekst",
            PublishImmediately = false
        };

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.False(result.IsPublished);
        Assert.Null(result.PublishedAt);
    }

    // ---------- UpdateAsync ----------

    [Fact]
    public async Task UpdateAsync_WhenArticleExists_UpdatesFields()
    {
        var existing = NewArticle(isPublished: true);

        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(existing.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        var service = new NewsArticleService(repositoryMock.Object);

        var request = new UpdateNewsArticleRequest
        {
            Title = "  Novi naslov  ",
            Summary = "  Novi opis  ",
            Content = "  Novi tekst  "
        };

        var result = await service.UpdateAsync(existing.Id, request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Novi naslov", result!.Title);
        Assert.Equal("Novi opis", result.Summary);
        Assert.Equal("Novi tekst", result.Content);

        repositoryMock.Verify(
            x => x.UpdateAsync(existing, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenArticleMissing_ReturnsNull()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((NewsArticle?)null);

        var service = new NewsArticleService(repositoryMock.Object);

        var request = new UpdateNewsArticleRequest
        {
            Title = "X",
            Summary = "Y",
            Content = "Z"
        };

        var result = await service.UpdateAsync(Guid.NewGuid(), request, CancellationToken.None);

        Assert.Null(result);
        repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<NewsArticle>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    // ---------- DeleteAsync ----------

    [Fact]
    public async Task DeleteAsync_WhenArticleExists_ReturnsTrue()
    {
        var existing = NewArticle(isPublished: false);

        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(existing.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.DeleteAsync(existing.Id, CancellationToken.None);

        Assert.True(result);
        repositoryMock.Verify(
            x => x.DeleteAsync(existing, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenArticleMissing_ReturnsFalse()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((NewsArticle?)null);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.DeleteAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.False(result);
        repositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<NewsArticle>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    // ---------- PublishAsync ----------

    [Fact]
    public async Task PublishAsync_WhenDraft_PublishesArticle()
    {
        var draft = NewArticle(isPublished: false);

        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(draft.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(draft);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.PublishAsync(draft.Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result!.IsPublished);
        Assert.NotNull(result.PublishedAt);

        repositoryMock.Verify(
            x => x.UpdateAsync(draft, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task PublishAsync_WhenAlreadyPublished_DoesNotUpdateAgain()
    {
        var published = NewArticle(isPublished: true);

        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(published.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(published);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.PublishAsync(published.Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result!.IsPublished);

        repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<NewsArticle>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task PublishAsync_WhenArticleMissing_ReturnsNull()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((NewsArticle?)null);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.PublishAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.Null(result);
    }

    // ---------- ArchiveAsync ----------

    [Fact]
    public async Task ArchiveAsync_WhenPublished_ArchivesArticle()
    {
        var published = NewArticle(isPublished: true);

        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(published.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(published);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.ArchiveAsync(published.Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result!.IsPublished);

        repositoryMock.Verify(
            x => x.UpdateAsync(published, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ArchiveAsync_WhenAlreadyDraft_DoesNotUpdate()
    {
        var draft = NewArticle(isPublished: false);

        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetByIdAsync(draft.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(draft);

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.ArchiveAsync(draft.Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(result!.IsPublished);

        repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<NewsArticle>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    // ---------- GetPublishedAsync (paginacija) ----------

    [Fact]
    public async Task GetPublishedAsync_NormalizesInvalidPaging()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetPublishedAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((new List<NewsArticle>(), 0));

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.GetPublishedAsync(0, 0, CancellationToken.None);

        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(0, result.TotalPages);
    }

    [Fact]
    public async Task GetPublishedAsync_CapsPageSizeAtFifty()
    {
        var repositoryMock = new Mock<INewsArticleRepository>();
        repositoryMock
            .Setup(x => x.GetPublishedAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((new List<NewsArticle>(), 100));

        var service = new NewsArticleService(repositoryMock.Object);

        var result = await service.GetPublishedAsync(1, 999, CancellationToken.None);

        Assert.Equal(50, result.PageSize);
        Assert.Equal(2, result.TotalPages);
    }
}