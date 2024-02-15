using Cumio.Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Cumio.Application.Features.Contents.EventHandlers;

public class ContentCreatedEventHandler : INotificationHandler<DomainEventNotification<ContentCreatedEvent>>
{
    private readonly ILogger<ContentCreatedEventHandler> _logger;

    public ContentCreatedEventHandler(ILogger<ContentCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<ContentCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("VerticalSlice Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}