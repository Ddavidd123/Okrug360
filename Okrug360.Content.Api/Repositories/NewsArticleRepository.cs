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

    public async Task<IReadOnlyList<NewsArticle>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.NewsArticles
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<NewsArticle?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext.NewsArticles
            .AsNoTracking()
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
}