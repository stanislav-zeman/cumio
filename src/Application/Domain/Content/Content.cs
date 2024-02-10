using Cumio.Application.Common;
using Cumio.Application.Common.Mappings;

namespace Cumio.Application.Domain.Todos;

public class Content : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int AuthorId { get; set; }
    public string? Url { get; set; }
    public int Length {  get; set; }
    public ContentType ContentType { get; set; }
    public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();
}

public class ContentSearchedEvent: DomainEvent
{
    public ContentSearchedEvent(Content item)
    {
       
    }

    public Content Item { get; }
}

public class ContentCreatedEvent : DomainEvent
{
    public ContentCreatedEvent(Content item)
    {

    }

    public Content Item { get; }
}

public class ContentEditedEvent : DomainEvent
{
    public ContentEditedEvent(Content item)
    {

    }

    public Content Item { get; }
}

public class ContentViewedEvent : DomainEvent
{
    public ContentViewedEvent(Content item)
    {

    }

    public Content Item { get; }
}

public class ContentSharedEvent : DomainEvent
{
    public ContentSharedEvent(Content item)
    {

    }

    public Content Item { get; }
}

public class ContentRecord : IMapFrom<Content>
{
    public string? Title { get; set; }
}

public enum ContentType { 
    Song, Podcast, Audiobook
}