using Cumio.Application.Common.Interfaces;
using Cumio.Application.Domain.ValueObjects;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace Cumio.Application.Infrastructure.Services;

public class AudioStorageService : IAudioStorageService
{
    private readonly StorageClient _client;
    private readonly string _bucket;

    public AudioStorageService()
    {
        _client = StorageClient.Create();
        _bucket = Guid.NewGuid().ToString();
    }

    public async Task<ObjectStorageLocation?> UploadAudioFileAsync(IFormFile file)
    {
        var objectName = Guid.NewGuid().ToString();
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        _client.UploadObject(_bucket, objectName, "audio/mpeg", stream);
        var location = new ObjectStorageLocation()
        {
            Bucket = _bucket,
            Filename = objectName,
        };
        return location;
    }

    public async Task<byte[]?> FetchAudioFileAsync(ObjectStorageLocation location)
    {
        using var stream = new MemoryStream();
        await _client.DownloadObjectAsync(location.Bucket, location.Filename, stream);
        return stream.ToArray();
    }
}