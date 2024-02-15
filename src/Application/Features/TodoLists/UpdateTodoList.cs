using Cumio.Application.Common;
using Cumio.Application.Common.Exceptions;
using Cumio.Application.Domain.Entities;
using Cumio.Application.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cumio.Application.Features.TodoLists;

public class UpdateTodoListController : ApiControllerBase
{
    [HttpPut("/api/todo-lists/{id}")]
    public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}

public class UpdateTodoListCommand : IRequest
{
    public int Id { get; set; }

    public string? Title { get; set; }
}

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateTodoListCommandValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
    {
        return _context.TodoLists
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

internal sealed class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateTodoListCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .FindAsync(new object[] { request.Id }, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException(nameof(TodoList), request.Id);

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}