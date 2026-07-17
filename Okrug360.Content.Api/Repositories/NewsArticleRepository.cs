using Microsoft.EntityFrameworkCore;
using Okrug360.Content.Api.Data;
using Okrug360.Content.Api.Entities;

namespace Okrug360.Content.Api.Repositories;

public sealed class NewsArticleRepository : INewsArticleRepository
{
    private readonly ContentDbContext _dbContext;

    public NewsArticleRepository(ContentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // CancellationToken: ako korisnik zatvori zahtev, ASP.NET može da prekine izvršavanje.
    public async Task<(IReadOnlyList<NewsArticle> Items, int TotalCount)> GetPublishedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.NewsArticles
            .AsNoTracking()
            .Where(x => x.IsPublished);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<NewsArticle?> GetPublishedByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.NewsArticles
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id && x.IsPublished,
                cancellationToken);
    }

    public async Task<NewsArticle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.NewsArticles
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        NewsArticle article,
        CancellationToken cancellationToken)
    {
        _dbContext.NewsArticles.Add(article);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        NewsArticle article,
        CancellationToken cancellationToken)
    {
        _dbContext.NewsArticles.Update(article);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        NewsArticle article,
        CancellationToken cancellationToken)
    {
        _dbContext.NewsArticles.Remove(article);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
