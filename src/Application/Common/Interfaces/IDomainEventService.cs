using Cumio.Application.Domain.Common;

namespace Cumio.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}