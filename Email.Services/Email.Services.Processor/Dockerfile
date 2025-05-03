# Base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8083
ENV ASPNETCORE_URLS=http://+:8083

# Build image using .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files for processor and Messaging
COPY ["Email.Services.Processor/Email.Services.Processor.csproj", "Email.Services.Processor/"]
COPY ["Email.Services.Messaging/Email.Services.Messaging.csproj", "Email.Services.Messaging/"]

# Restore dependencies for both projects
RUN dotnet restore "Email.Services.Processor/Email.Services.Processor.csproj"

# Copy all source code (processor, Messaging, and other projects)
COPY . .

# Build and publish the application
RUN dotnet publish "Email.Services.Processor/Email.Services.Processor.csproj" -c Release -o /app/publish -v d

# Use the runtime-only image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Email.Services.Processor.dll"]