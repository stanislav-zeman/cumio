using Cumio.Application.Common;

namespace Cumio.Application.Domain.Entities;

public class Collection : BaseAuditableEntity
{
    public string? Title { get; set; }

    public string? Author { get; set; }

    public IList<Content> Contents { get; private set; } = new List<Content>();
}