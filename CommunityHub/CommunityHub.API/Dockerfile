

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["CommunityHub.API/CommunityHub.API.csproj", "CommunityHub.API/"]
COPY ["../CommunityHub.Application/CommunityHub.Application.csproj", "../CommunityHub.Application/"]
COPY ["../CommunityHub.Contracts/CommunityHub.Contracts.csproj", "../CommunityHub.Contracts/"]
COPY ["../CommunityHub.Domain/CommunityHub.Domain.csproj", "../CommunityHub.Domain/"]
COPY ["../CommunityHub.Infrastructure/CommunityHub.Infrastructure.csproj", "../CommunityHub.Infrastructure/"]

RUN dotnet restore "CommunityHub.API/CommunityHub.API.csproj"

COPY . .

WORKDIR "/src/CommunityHub.API"
RUN dotnet build "CommunityHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/CommunityHub.API"
RUN dotnet publish "CommunityHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommunityHub.API.dll"]