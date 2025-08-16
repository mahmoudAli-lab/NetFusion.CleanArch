# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy sln and restore
COPY NetFusion.CleanArch.sln ./
COPY src/NetFusion.Api/NetFusion.Api.csproj src/NetFusion.Api/
COPY src/NetFusion.Application/NetFusion.Application.csproj src/NetFusion.Application/
COPY src/NetFusion.Domain/NetFusion.Domain.csproj src/NetFusion.Domain/
COPY src/NetFusion.Infrastructure/NetFusion.Infrastructure.csproj src/NetFusion.Infrastructure/
RUN dotnet restore

# copy the rest and publish
COPY . .
RUN dotnet publish src/NetFusion.Api/NetFusion.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
# (optional) HTTPS if you enable Kestrel HTTPS
# EXPOSE 8443

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Docker

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NetFusion.Api.dll"]
