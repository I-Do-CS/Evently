# Evently

A .NET 10 event API organized as a modular monolith. The Events module owns its domain model, commands and queries, routes, and PostgreSQL persistence. The host wires the module into ASP.NET Core.

## Run with Docker Compose

From the repository root:

```bash
docker compose -p evently up --build
```

Compose starts PostgreSQL and waits for its health check before starting the API. The Development host applies migrations and serves the [OpenAPI document](http://localhost:5000/openapi/v1.json).

Create an event:

```bash
curl http://localhost:5000/events \
  -H 'Content-Type: application/json' \
  -d '{
    "title": "Systems meetup",
    "description": "Short talks and demos",
    "location": "Tehran",
    "startsAtUtc": "2030-06-01T17:00:00Z",
    "endsAtUtc": "2030-06-01T19:00:00Z"
  }'
```

The response contains the event ID. Use it in `GET /events/{id}` to retrieve the event.

## Follow the Events module

- [Presentation](Modules/Events/Evently.Modules.Events.Presentation/Events/) maps HTTP requests to the module’s commands and queries.
- [Application](Modules/Events/Evently.Modules.Events.Application/Events/) contains the create and read handlers.
- [Domain](Modules/Events/Evently.Modules.Events.Domain/Events/) defines the event entity, its status and the event-created domain event.
- [Infrastructure](Modules/Events/Evently.Modules.Events.Infrastructure/) registers the module and owns its database context and migrations.

Course project in modular backend architecture.
