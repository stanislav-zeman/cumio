using Cumio.Application.Common.Interfaces;
using Cumio.Application.Domain.Entities;
using Cumio.Application.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cumio.Application.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<Content> Contents => Set<Content>();

    public DbSet<Collection> Collections => Set<Collection>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>()
            .Property(c => c.Title)
            .HasMaxLength(256);

        modelBuilder.Entity<Collection>()
            .Property(c => c.Title)
            .HasMaxLength(256);


        modelBuilder.Entity<Collection>()
            .Property(c => c.Description)
            .HasMaxLength(1024);

        base.OnModelCreating(modelBuilder);
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("CumioDB");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}