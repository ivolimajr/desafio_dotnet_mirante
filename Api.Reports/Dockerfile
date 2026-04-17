# Estágio de Execução (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
# O Cloud Run exige que a aplicação escute na porta 8080
ENV ASPNETCORE_URLS=http://+:8080
# Garante que o Swagger e as configurações de produção sejam carregadas corretamente
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
EXPOSE 8080

# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copia os arquivos de projeto individualmente para aproveitar o cache do Docker
# Caminhos relativos à raiz da solução
COPY ["Api.Reports/Api.Reports.csproj", "Api.Reports/"]
COPY ["reports.infrastructure/reports.infrastructure.csproj", "reports.infrastructure/"]

# 2. Restaura as dependências. O .NET resolve a referência entre os projetos aqui
RUN dotnet restore "Api.Reports/Api.Reports.csproj"

# 3. Copia todo o código fonte da solução
COPY . .

# 4. Define o diretório de trabalho na pasta da API e compila
WORKDIR "/src/Api.Reports"
RUN dotnet build "Api.Reports.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio de Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.Reports.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio Final (Imagem enxuta)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Reports.dll"]