# 1. Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar os csproj dos projetos
COPY src/Hypesoft.API/Hypesoft.API.csproj Hypesoft.API/
COPY src/Hypesoft.Application/Hypesoft.Application.csproj Hypesoft.Application/
COPY src/Hypesoft.Infrastructure/Hypesoft.Infrastructure.csproj Hypesoft.Infrastructure/
COPY src/Hypesoft.Domain/Hypesoft.Domain.csproj Hypesoft.Domain/

# Copiar o restante do código
COPY src/ .

# Restaurar dependências
RUN dotnet restore Hypesoft.API/Hypesoft.API.csproj

# Publicar
RUN dotnet publish Hypesoft.API/Hypesoft.API.csproj -c Release -o /app/publish

# 2. Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Hypesoft.API.dll"]
