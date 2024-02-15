using Cumio.Application.Common.Exceptions;
using Cumio.Application.Common.Interfaces;
using Cumio.Application.Domain.Entities;
using AutoMapper;
using Cumio.Application.Common;
using Cumio.Application.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cumio.Application.Features.Contents;

public class DownloadContentController : ApiControllerBase
{
    [HttpGet("/api/content/{id}/download")]
    public async Task<ActionResult<FileResult>> Update(string id, DownloadContentQuery query)
    {
        if (id != query.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(query);

        return NoContent();
    }

}

public class DownloadContentQuery : IRequest<FileResult>
{
    public string? Id { get; set; }
}

internal sealed class DownloadContentHandler : IRequestHandler<DownloadContentQuery, FileResult>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAudioStorageService _audioStorageService;

    public DownloadContentHandler(ApplicationDbContext context, IMapper mapper, IAudioStorageService storageService)
    {
        _context = context;
        _mapper = mapper;
        _audioStorageService = storageService;
    }

    public async Task<FileResult> Handle(DownloadContentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Contents
            .FindAsync(new object[] { request.Id! }, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Content), request.Id!);

        var data = await _audioStorageService.FetchAudioFileAsync(entity.DataLocation);
        return new FileContentResult(data, "audio/mpeg");
    }
}