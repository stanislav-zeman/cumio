using Cumio.Application.Common;
using Cumio.Application.Common.Exceptions;
using Cumio.Application.Domain.Entities;
using Cumio.Application.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cumio.Application.Features.TodoItems;

public class DeleteTodoItemController : ApiControllerBase
{
    [HttpDelete("/api/todo-items/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand { Id = id });

        return NoContent();
    }
}

public class DeleteTodoItemCommand : IRequest
{
    public int Id { get; set; }
}

internal sealed class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException(nameof(TodoItem), request.Id);
        _context.TodoItems.Remove(entity);

        entity.DomainEvents.Add(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

public class TodoItemDeletedEvent : DomainEvent
{
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}