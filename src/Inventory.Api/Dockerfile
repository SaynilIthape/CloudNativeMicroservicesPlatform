
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/Inventory.Api/Inventory.Api.csproj", "src/Inventory.Api/"]
RUN dotnet restore "src/Inventory.Api/Inventory.Api.csproj"

COPY . .
WORKDIR /src/src/Inventory.Api
RUN dotnet publish "Inventory.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "Inventory.Api.dll"]
