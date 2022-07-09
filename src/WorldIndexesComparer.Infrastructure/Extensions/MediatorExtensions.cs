using MediatR;
using WorldIndexesComparer.Domain;

namespace WorldIndexesComparer.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, Entity entity, CancellationToken cancellationToken)
    {
        if (entity.DomainEvents is null || !entity.DomainEvents.Any())
        {
            return;
        }

        foreach (var @event in entity.DomainEvents)
        {
            if (@event is INotification notification)
            {
                await mediator.Publish(notification, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
            }
        }        

        entity.ClearDomainEvents();
    }
}