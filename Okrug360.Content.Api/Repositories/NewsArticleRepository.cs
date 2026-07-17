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

    //cancellation token se koristi zbog stednje resursa
    //akko korisnik zatvori stranicu, asp net - prekini izvrsavanje
    public async Task<IReadOnlyList<NewsArticle>> GetPublishedAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.NewsArticles
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.PublishedAt)
            .ToListAsync(cancellationToken);
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

    public async Task AddAsync(
        NewsArticle article,
        CancellationToken cancellationToken)
    {
        _dbContext.NewsArticles.Add(article);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}