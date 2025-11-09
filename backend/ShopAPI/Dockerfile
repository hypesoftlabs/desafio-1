# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia tudo do backend para dentro da imagem
COPY . .

# Vai para a pasta do projeto WebAPI
WORKDIR /src/ShopAPI

# Publica o projeto (o publish já faz restore automaticamente)
RUN dotnet publish "ShopAPI.csproj" -c Release -o /app/publish

# Estágio 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "ShopAPI.dll"]
