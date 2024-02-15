using Cumio.Application.Common;
using Cumio.Application.Common.Exceptions;
using Cumio.Application.Domain.Entities;
using Cumio.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cumio.Application.Features.Collections.Contents;

public class CreateCollectionContentController : ApiControllerBase
{
    [HttpPost("/api/collection/{collectionId}/content/{contentId}")]
    public async Task<ActionResult> Update(string collectionId, string contentId, CreateCollectionContentCommand command)
    {
        if (collectionId != command.CollectionId || contentId != command.ContentId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}

public class CreateCollectionContentCommand : IRequest
{
    public string? CollectionId { get; set; }
    public string? ContentId { get; set; }

}

public class UpdateTodoListCommandValidator : AbstractValidator<CreateCollectionContentCommand>
{
    public UpdateTodoListCommandValidator()
    {
        RuleFor(v => v.CollectionId)
            .NotNull()
            .NotEmpty();

        RuleFor(v => v.CollectionId)
            .NotNull()
            .NotEmpty();
    }
}

internal sealed class CreateCollectionContentCommandHandler : IRequestHandler<CreateCollectionContentCommand>
{
    private readonly ApplicationDbContext _context;

    public CreateCollectionContentCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateCollectionContentCommand request, CancellationToken cancellationToken)
    {
        var collection = await _context.Collections
            .FindAsync(new object[] { request.CollectionId! }, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Collection), request.CollectionId!);

         var content = await _context.Contents
                 .FindAsync(new object[] { request.ContentId! }, cancellationToken)
                 .ConfigureAwait(false) ?? throw new NotFoundException(nameof(Collection), request.ContentId!);

        collection.Contents.Add(content);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}