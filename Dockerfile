FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Api.Reports/Api.Reports.csproj", "Api.Reports/"]
COPY ["reports.application/reports.application.csproj", "reports.application/"]
COPY ["reports.domain/reports.domain.csproj", "reports.domain/"]
COPY ["reports.infrastructure/reports.infrastructure.csproj", "reports.infrastructure/"]

RUN dotnet restore "Api.Reports/Api.Reports.csproj"

COPY . .

WORKDIR "/src/Api.Reports"
RUN dotnet publish "Api.Reports.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.Reports.dll"]