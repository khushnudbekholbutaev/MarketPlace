FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

EXPOSE 8080

# Loyiha csproj fayllarini nusxalash
COPY TechStation.Api/*.csproj TechStation.Api/
COPY TechStation.Service/*.csproj TechStation.Service/
COPY TechStation.Domain/*.csproj TechStation.Domain/
COPY TechStation.Data/*.csproj TechStation.Data/

# Restore bajarish
RUN dotnet restore TechStation.Api/TechStation.Api.csproj

# Barcha kodni nusxalash
COPY . .

# Build qilish
RUN dotnet build TechStation.Api/TechStation.Api.csproj -c Release -o /app/build

# Migrationlarni bajarish (database update)
RUN dotnet ef database update --project ./TechStation.Data --startup-project ./TechStation.Api

# Publish qilish
RUN dotnet publish TechStation.Api/TechStation.Api.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TechStation.Api.dll"]
