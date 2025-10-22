# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia tudo para dentro da imagem
COPY . .

# Restaura as dependências e compila em modo Release
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expõe a porta da API
EXPOSE 8080

# Inicia a aplicação
ENTRYPOINT ["dotnet", "APIChat.dll"]
