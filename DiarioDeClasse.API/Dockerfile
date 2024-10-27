FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DiarioDeClasse.API/DiarioDeClasse.API.csproj", "DiarioDeClasse.API/"]
COPY ["DiarioDeClasse.Domain/DiarioDeClasse.Domain.csproj", "DiarioDeClasse.Domain/"]
COPY ["DiarioDeClasse.Infra/DiarioDeClasse.Infra.csproj", "DiarioDeClasse.Infra/"]
COPY ["DiarioDeClasse.Repository/DiarioDeClasse.Repository.csproj", "DiarioDeClasse.Repository/"]
RUN dotnet restore "./DiarioDeClasse.API/DiarioDeClasse.API.csproj"
COPY . .
WORKDIR "/src/DiarioDeClasse.API"
RUN dotnet build "./DiarioDeClasse.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DiarioDeClasse.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiarioDeClasse.API.dll"]