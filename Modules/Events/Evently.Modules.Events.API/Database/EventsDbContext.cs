using Evently.Modules.Events.API.Events;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.API.Database;

public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options)
{
    internal DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.EventsSchema);
    }
}
