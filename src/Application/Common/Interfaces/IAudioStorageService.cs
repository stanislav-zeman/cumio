using Microsoft.AspNetCore.Http;

namespace Cumio.Application.Common.Interfaces;

public interface IAudioStorageService
{

    Task<string?> UploadAudioFileAsync(IFormFile file);

    Task<IFormFile?> FetchAudioFileAsync(string sourceId);
}