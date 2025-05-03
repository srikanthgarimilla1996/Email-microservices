FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081
# Use the official .NET SDK image as the base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the project file
COPY ["Email.Services.Gateway.csproj", "./"]

# Restore dependencies
RUN dotnet restore "Email.Services.Gateway.csproj"

# Copy the entire project
COPY . ./

# Build the application
RUN dotnet publish "Email.Services.Gateway.csproj" -c Release -o /app/publish

# Use the runtime-only image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Email.Services.Gateway.dll"]
