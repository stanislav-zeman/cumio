using Cumio.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cumio.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Content> Contents { get; }

    DbSet<Collection> Collections { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}