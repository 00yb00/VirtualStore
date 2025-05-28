# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files
COPY VirtualStore.Managers/VirtualStore.Managers.csproj VirtualStore.Managers/
COPY VirtualStore.ServiceDefaults/VirtualStore.ServiceDefaults.csproj VirtualStore.ServiceDefaults/

# Restore
RUN dotnet restore VirtualStore.Managers/VirtualStore.Managers.csproj

# Copy the full source tree
COPY . .

# Publish
WORKDIR /src/VirtualStore.Managers
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VirtualStore.Managers.dll"]
