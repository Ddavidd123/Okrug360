using Microsoft.EntityFrameworkCore;
using Okrug360.Places.Api.Entities;
using Okrug360.Places.Api.Enums;
namespace Okrug360.Places.Api.Data
{
    public class PlacesDbContext : DbContext
    {
        public PlacesDbContext(DbContextOptions<PlacesDbContext> options)
            : base (options)
        {

        }

        public DbSet<Place> Places => Set<Place>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ovo kaze da sledeca podesavanja vaze za model
            //Place
            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(x => x.Name).HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(4000);
                entity.Property(x => x.Address).HasMaxLength(300);
                entity.Property(x => x.City).HasMaxLength(100);
                entity.Property(x => x.ImageUrl).HasMaxLength(500);
                entity.Property(x => x.Category)
               .HasConversion<string>()
               .HasMaxLength(50);
            });
        }

    }
}
