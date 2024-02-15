using Cumio.Application.Common;
using Cumio.Application.Domain.ValueObjects;

namespace Cumio.Application.Domain.Entities;

public class Content : BaseAuditableEntity
{
    public string? Title { get; set; }

    public string? Author { get; set; }

    public ObjectStorageLocation? DataLocation { get; set; }
}