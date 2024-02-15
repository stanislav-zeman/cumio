using Cumio.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cumio.Application.Infrastructure.Services;

public class AudioStorageService : IAudioStorageService
{
    public Task<string?> UploadAudioFileAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<IFormFile?> FetchAudioFileAsync(string sourceId)
    {
        throw new NotImplementedException();
    }
}