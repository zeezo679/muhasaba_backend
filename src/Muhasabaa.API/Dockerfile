FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Muhasabaa.API/Muhasabaa.API.csproj", "src/Muhasabaa.API/"]
COPY ["src/Muhasabaa.Application/Muhasabaa.Application.csproj", "src/Muhasabaa.Application/"]
COPY ["src/Muhasabaa.Domain/Muhasabaa.Domain.csproj", "src/Muhasabaa.Domain/"]
COPY ["src/Muhasabaa.Infrastructure/Muhasabaa.Infrastructure.csproj", "src/Muhasabaa.Infrastructure/"]

RUN dotnet restore "src/Muhasabaa.API/Muhasabaa.API.csproj"
COPY . .
WORKDIR "/src/src/Muhasabaa.API"
RUN dotnet publish "./Muhasabaa.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Muhasabaa.API.dll"]