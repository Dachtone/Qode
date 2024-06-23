# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-stage

# Work directory
WORKDIR /app

# Copy source files
COPY . ./

# Gather dependencies
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o out

# Test stage
FROM build-stage as test-stage

# Run unit tests
RUN dotnet test /app/Qode.sln --nologo

# Run stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Work directory
WORKDIR /app

# Copy build files
COPY --from=build-stage /App/out .

# Run entry project
ENTRYPOINT ["dotnet", "Qode.UI.dll"]
