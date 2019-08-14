FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../auth-demo-gateway-api/auth-demo-gateway-api.csproj", "../auth-demo-gateway-api/"]
RUN dotnet restore "../auth-demo-gateway-api/auth-demo-gateway-api.csproj"
COPY . .
WORKDIR "/src/../auth-demo-gateway-api"
RUN dotnet build "auth-demo-gateway-api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "auth-demo-gateway-api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "auth-demo-gateway-api.dll"]