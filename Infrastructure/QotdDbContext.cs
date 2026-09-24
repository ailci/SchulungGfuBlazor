using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class QotdDbContext(DbContextOptions<QotdDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Quote> Quotes => Set<Quote>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.SeedData();
    }
}