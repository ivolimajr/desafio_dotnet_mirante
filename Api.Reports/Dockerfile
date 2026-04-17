FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# When building from the repository root as the build context, copy the project files
# using their folder paths so dependencies can be restored before copying the full source.
COPY ["Api.Reports/Api.Reports.csproj", "Api.Reports/"]
COPY ["reports.infrastructure/reports.infrastructure.csproj", "reports.infrastructure/"]
RUN dotnet restore "Api.Reports/Api.Reports.csproj"

COPY . .
WORKDIR /src/Api.Reports
RUN dotnet build "Api.Reports.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Api.Reports
RUN dotnet publish "Api.Reports.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Reports.dll"]
