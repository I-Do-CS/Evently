FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=5000
EXPOSE 5000

USER app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the API project file first.
COPY ["API/Evently.API/Evently.API.csproj", "API/Evently.API/"]

# Add a COPY instruction for every project referenced by the API.
COPY ["Modules/Events/Evently.Modules.Events.Application/Evently.Modules.Events.Application.csproj", "Modules/Events/Evently.Modules.Events.Application/"]
COPY ["Modules/Events/Evently.Modules.Events.Domain/Evently.Modules.Events.Domain.csproj", "Modules/Events/Evently.Modules.Events.Domain/"]
COPY ["Modules/Events/Evently.Modules.Events.Infrastructure/Evently.Modules.Events.Infrastructure.csproj", "Modules/Events/Evently.Modules.Events.Infrastructure/"]
COPY ["Modules/Events/Evently.Modules.Events.Presentation/Evently.Modules.Events.Presentation.csproj", "Modules/Events/Evently.Modules.Events.Presentation/"]

COPY [".editorconfig", "./"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["global.json", "./"]

# Restore dependencies using a persistent NuGet package cache.
RUN --mount=type=cache,id=evently-nuget,target=/root/.nuget/packages \
    dotnet restore "API/Evently.API/Evently.API.csproj"

# Copy application code only after restoring dependencies.
COPY . .

# Publish without restoring packages again.
# Reuse the same cache mount so the restored packages are available.
RUN --mount=type=cache,id=evently-nuget,target=/root/.nuget/packages \
    dotnet publish "API/Evently.API/Evently.API.csproj" \
    -c "$BUILD_CONFIGURATION" \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Evently.API.dll"]