# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files
COPY VirtualStore.Accessor/VirtualStore.Accessor.csproj VirtualStore.Accessor/
COPY VirtualStore.ServiceDefaults/VirtualStore.ServiceDefaults.csproj VirtualStore.ServiceDefaults/

# Restore
RUN dotnet restore VirtualStore.Accessor/VirtualStore.Accessor.csproj

# Copy the full source tree
COPY . .

# Publish
WORKDIR /src/VirtualStore.Accessor
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VirtualStore.Accessor.dll"]
