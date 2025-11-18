# ===========================
# 1. Build Stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# Copy proj file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy everything and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ===========================
# 2. Runtime Stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose Render's port
EXPOSE 10000

# Render provides PORT environment variable
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

# Run the app
ENTRYPOINT ["dotnet", "OnlineFoodOrderingSystem.dll"]
  
