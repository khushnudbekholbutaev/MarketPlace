FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TechStation.Api/TechStation.Api.csproj", "src/TechStation.Api/"]
COPY ["src/TechStation.Data/TechStation.Data.csproj", "src/TechStation.Data/"]
COPY ["src/TechStation.Domain/TechStation.Domain.csproj", "src/TechStation.Domain/"]
COPY ["src/TechStation.Service/TechStation.Service.csproj", "src/TechStation.Service/"]
RUN dotnet restore "./src/TechStation.Api/TechStation.Api.csproj"
COPY . .
WORKDIR "/src/src/TechStation.Api"
RUN dotnet build "./TechStation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechStation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechStation.Api.dll"]