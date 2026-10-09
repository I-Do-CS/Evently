using Evently.Modules.Events.API.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.API.Events;

public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        // This stuff is truly majestic my nigga
        app.MapGet(
                "events/{id}",
                async (Guid id, EventsDbContext context) =>
                {
                    Event? e = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

                    EventResponse? @event = e is null
                        ? null
                        : new EventResponse(
                            e.Id,
                            e.Title,
                            e.Description,
                            e.Location,
                            e.StartsAtUtc,
                            e.EndsAtUtc
                        );

                    return @event is null ? Results.NotFound() : Results.Ok(@event);
                }
            )
            .WithTags(Tags.Events);
    }
}
