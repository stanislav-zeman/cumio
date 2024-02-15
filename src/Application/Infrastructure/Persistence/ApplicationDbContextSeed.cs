using Cumio.Application.Domain.Entities;
using Cumio.Application.Domain.ValueObjects;

namespace Cumio.Application.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        await context.SaveChangesAsync();
    }
}