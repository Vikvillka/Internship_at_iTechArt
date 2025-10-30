FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommunityHub/Gateway.API/Gateway.API.csproj", "Gateway.API/"]

COPY ["packages", "/src/packages"]

RUN dotnet restore "Gateway.API/Gateway.API.csproj" \
    --source /src/packages \
    --source https://api.nuget.org/v3/index.json

COPY CommunityHub/Gateway.API ./Gateway.API

WORKDIR "/src/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/Gateway.API"
RUN dotnet publish "Gateway.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]