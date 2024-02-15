using Cumio.Application.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Cumio.Application.Common.Interfaces;

public interface IAudioStorageService
{

    Task<ObjectStorageLocation?> UploadAudioFileAsync(IFormFile file);

    Task<byte[]?> FetchAudioFileAsync(ObjectStorageLocation location);
}