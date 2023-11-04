# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
WORKDIR /app

WORKDIR /src

COPY ["NexTube.Application/NexTube.Application.csproj", "NexTube.Application/"]
COPY ["NexTube.Domain/NexTube.Domain.csproj", "NexTube.Domain/"]
COPY ["NexTube.Persistence/NexTube.Persistence.csproj", "NexTube.Persistence/"]
COPY ["NexTube.WebApi/NexTube.WebApi.csproj", "NexTube.WebApi/"]
RUN dotnet restore "NexTube.WebApi/NexTube.WebApi.csproj"

# copy everything else and build app
COPY . .
WORKDIR /src/NexTube.WebApi
RUN dotnet build "NexTube.WebApi.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "NexTube.WebApi.csproj" -c Release -o /app/publish


# final stage/image
#FROM mcr.microsoft.com/dotnet/aspnet:7.0
#WORKDIR /app
#COPY --from=build /app/publish .
#ENTRYPOINT ["dotnet", "NexTube.WebApi.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NexTube.WebApi.dll"]
