#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WorldIndexesComparer.BackgroundServices/WorldIndexesComparer.BackgroundServices.csproj", "src/WorldIndexesComparer.BackgroundServices/"]
COPY ["src/WorldIndexesComparer.Infrastructure/WorldIndexesComparer.Infrastructure.Data.csproj", "src/WorldIndexesComparer.Infrastructure/"]
COPY ["src/WorldIndexesComparer.Domain/WorldIndexesComparer.Domain.csproj", "src/WorldIndexesComparer.Domain/"]
COPY ["src/RestCountries.Client/RestCountries.Client.csproj", "src/RestCountries.Client/"]
COPY ["src/WorldIndexesComparer.Common.Http/WorldIndexesComparer.Common.Http.csproj", "src/WorldIndexesComparer.Common.Http/"]
COPY ["src/WorldIndexesComparer.Application/WorldIndexesComparer.Application.csproj", "src/WorldIndexesComparer.Application/"]
COPY ["src/Covid19.Client/Covid19.Client.csproj", "src/Covid19.Client/"]
COPY ["src/Econdb.Client/Econdb.Client.csproj", "src/Econdb.Client/"]
RUN dotnet restore "src/WorldIndexesComparer.BackgroundServices/WorldIndexesComparer.BackgroundServices.csproj"
COPY . .
WORKDIR "/src/src/WorldIndexesComparer.BackgroundServices"
RUN dotnet build "WorldIndexesComparer.BackgroundServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorldIndexesComparer.BackgroundServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorldIndexesComparer.BackgroundServices.dll"]