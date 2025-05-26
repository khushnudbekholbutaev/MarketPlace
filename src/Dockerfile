FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

EXPOSE 8080

# Barcha loyiha fayllarini nusxa ko'chirish
COPY TechStation.Api/*.csproj TechStation.Api/
COPY TechStation.Service/*.csproj TechStation.Service/
COPY TechStation.Domain/*.csproj TechStation.Domain/
COPY TechStation.Data/*.csproj TechStation.Data/

# Restore qilamiz (bu barcha loyihalar uchun bo'ladi)
RUN dotnet restore TechStation.Api/TechStation.Api.csproj

# Barcha kodni nusxa ko'chiramiz
COPY . .

# Build qilamiz
RUN dotnet build TechStation.Api/TechStation.Api.csproj -c Release -o /app/build

# Publish qilamiz
FROM build AS publish
RUN dotnet publish TechStation.Api/TechStation.Api.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechStation.Api.dll"]
