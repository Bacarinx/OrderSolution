# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore OrderSolution.API/OrderSolution.API.csproj
RUN dotnet publish -c Release -o /app/publish OrderSolution.API/OrderSolution.API.csproj

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# ✅ Instala o netcat
RUN apt-get update && apt-get install -y netcat-traditional && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# ✅ Copia o wait-for-it.sh e dá permissão de execução
COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

EXPOSE 5194

# ✅ Usa o wait-for-it.sh como entrypoint
ENTRYPOINT ["/wait-for-it.sh", "db", "1433", "--", "dotnet", "OrderSolution.API.dll"]
