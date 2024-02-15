using Cumio.Application.Common;
using Cumio.Application.Domain.Entities;
using Cumio.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cumio.Application.Features.Collections;

public class CreateCollectionController : ApiControllerBase
{
    [HttpPost("/api/collections")]
    public async Task<ActionResult<int>> Create(CreateCollectionCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreateCollectionCommand : IRequest<int>
{
    public string? Title { get; set; }

    public string? Description { get; set; }
}

public class CreateCollectionCommandValidator : AbstractValidator<CreateCollectionCommand>
{

    public CreateCollectionCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
    }
}

internal sealed class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateCollectionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Collection()
        {
            Title = request.Title,
            Description = request.Description,
        };

        _context.Collections.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}