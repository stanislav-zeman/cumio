using Cumio.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cumio.Application.Features.Contents;

using Common;
using Domain.Entities;
using Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class CreateContentController : ApiControllerBase
{
    [HttpPost("/api/contents")]
    public async Task<ActionResult<int>> Create(CreateContentCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreateContentCommand : IRequest<int>
{
    public string? Title { get; set; }

    public IFormFile? InputFile { get; set; }
}

public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
{
    public CreateContentCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(v => v.InputFile)
            .NotNull()
            .NotEmpty()
            .Must(f => f?.ContentType is "audio/mpeg" or "audio/wav");
    }
}

internal sealed class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, int>
{
    private readonly ApplicationDbContext _context;
    private readonly IAudioStorageService _audioStorageService;

    public CreateContentCommandHandler(ApplicationDbContext context, IAudioStorageService audioStorageService)
    {
        _context = context;
        _audioStorageService = audioStorageService;
    }

    public async Task<int> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {

        var dataUrl = await _audioStorageService.UploadAudioFileAsync(request.InputFile!);

        var entity = new Content()
        {
            Title = request.Title,
            DataUrl = dataUrl
        };

        entity.AddDomainEvent(new ContentCreatedEvent(entity));

        _context.Contents.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

public class ContentCreatedEvent : DomainEvent
{
    public ContentCreatedEvent(Content item)
    {
        Item = item;
    }

    public Content Item { get; }
}