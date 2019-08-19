FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_8.x | bash - && \
    apt-get install -y build-essential nodejs
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../auth-demo-gateway-angularspa/auth-demo-gateway-angularspa.csproj", "../auth-demo-gateway-angularspa/"]
RUN dotnet restore "../auth-demo-gateway-angularspa/auth-demo-gateway-angularspa.csproj"
COPY . .
WORKDIR "/src/../auth-demo-gateway-angularspa"
RUN dotnet build "auth-demo-gateway-angularspa.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "auth-demo-gateway-angularspa.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "auth-demo-gateway-angularspa.dll"]