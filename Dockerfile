# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar arquivos de projeto de todos os subprojetos
COPY ./api/src/Hypesoft.Domain/Hypesoft.Domain.csproj ./Hypesoft.Domain/
COPY ./api/src/Hypesoft.Application/Hypesoft.Application.csproj ./Hypesoft.Application/
COPY ./api/src/Hypesoft.Infrastructure/Hypesoft.Infrastructure.csproj ./Hypesoft.Infrastructure/
COPY ./api/src/Hypesoft.API/Hypesoft.API.csproj ./Hypesoft.API/

# Restaurar dependências
RUN dotnet restore ./Hypesoft.API/Hypesoft.API.csproj --disable-parallel --no-cache

# Copiar o restante do código fonte
COPY ./api/src/Hypesoft.Domain ./Hypesoft.Domain
COPY ./api/src/Hypesoft.Application ./Hypesoft.Application
COPY ./api/src/Hypesoft.Infrastructure ./Hypesoft.Infrastructure
COPY ./api/src/Hypesoft.API ./Hypesoft.API

# Entrar no projeto da API e publicar
WORKDIR /src/Hypesoft.API
RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar build publicado
COPY --from=build /app ./

# Expor as portas
EXPOSE 8080
EXPOSE 8081

# Variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:8080;https://+:8081

ENTRYPOINT ["dotnet", "Hypesoft.API.dll"]
