FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TABP.Api/TABP.Api.csproj", "src/TABP.Api/"]
COPY ["src/TABP.Application/TABP.Application.csproj", "src/TABP.Application/"]
COPY ["src/TABP.Domain/TABP.Domain.csproj", "src/TABP.Domain/"]
COPY ["src/TABP.Infrastructure/TABP.Infrastructure.csproj", "src/TABP.Infrastructure/"]
COPY ["src/TABP.Shared/TABP.Shared.csproj", "src/TABP.Shared/"]
RUN dotnet restore "src/TABP.Api/TABP.Api.csproj"
COPY . .
WORKDIR "/src/src/TABP.Api"
RUN dotnet build "TABP.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TABP.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TABP.Api.dll"]
