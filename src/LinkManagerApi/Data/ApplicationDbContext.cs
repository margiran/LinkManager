using System.Collections.Immutable;
using LinkManagerApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace LinkManagerApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {


    }


    public DbSet<Link> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Link>()
            .HasQueryFilter(l => !l.IsDelete);
    }
}
