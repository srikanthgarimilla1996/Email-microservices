# Base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8082
ENV ASPNETCORE_URLS=http://+:8082
# Build image using .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files for API and Messaging
COPY ["Email.Services.API/Email.Services.API.csproj", "Email.Services.API/"]
COPY ["Email.Services.Messaging/Email.Services.Messaging.csproj", "Email.Services.Messaging/"]

# Restore dependencies for both projects
RUN dotnet restore "Email.Services.API/Email.Services.API.csproj"

# Copy all source code (API, Messaging, and other projects)
COPY . .

# Build and publish the application
RUN dotnet publish "Email.Services.API/Email.Services.API.csproj" -c Release -o /app/publish -v d

# Use the runtime-only image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Email.Services.API.dll"]