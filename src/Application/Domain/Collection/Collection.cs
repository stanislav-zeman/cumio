using Cumio.Application.Common;
using Cumio.Application.Common.Mappings;

namespace Cumio.Application.Domain.Todos;

public class Collection : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public string? Title { get; set; }

    public List<int> Contents { get; set; } = new List<int>();
    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}

public class CollectionCreatedEvent : DomainEvent
{
    public CollectionCreatedEvent(Collection item)
    {

    }

    public Collection Item { get; }
}

public class CollectionEditedEvent : DomainEvent
{
    public CollectionEditedEvent(Collection item)
    {

    }

    public Collection Item { get; }
}

public class CollectionDeletedEvent : DomainEvent
{
    public CollectionDeletedEvent(Collection item)
    {

    }

    public Collection Item { get; }
}
public class TodoItemRecord : IMapFrom<Collection>
{
    public string? Title { get; set; }

}