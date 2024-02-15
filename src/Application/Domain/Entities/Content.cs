using Cumio.Application.Common;

namespace Cumio.Application.Domain.Entities;

public class Content : BaseAuditableEntity
{
    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? DataUrl { get; set; }
}