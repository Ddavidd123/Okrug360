using Microsoft.EntityFrameworkCore;
using Okrug360.Content.Api.Entities;
using System.Collections.Generic;

namespace Okrug360.Content.Api.Data;

public class ContentDbContext : DbContext
{
    public ContentDbContext(DbContextOptions<ContentDbContext> options)
    : base(options)
    {
    }

    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
}