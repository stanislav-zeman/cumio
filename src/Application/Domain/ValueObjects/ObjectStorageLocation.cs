using Microsoft.EntityFrameworkCore;

namespace Cumio.Application.Domain.ValueObjects;

[Owned]
public class ObjectStorageLocation
{
    public string? Bucket { get; set; }

    public string? Filename { get; set; }
}