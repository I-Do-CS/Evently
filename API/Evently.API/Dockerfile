FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copy build configuration and project metadata first.
COPY ["global.json", "."]
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["API/Evently.API/Evently.API.csproj", "API/Evently.API/"]

# Restore dependencies before copying source code.
RUN dotnet restore "API/Evently.API/Evently.API.csproj"

# Copy the remaining source files.
COPY . .

# Publish the application.
RUN dotnet publish "API/Evently.API/Evently.API.csproj" \
    --configuration $BUILD_CONFIGURATION \
    --output /app/publish \
    --no-restore \
    /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=5000

EXPOSE 5000

USER $APP_UID

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Evently.API.dll"]