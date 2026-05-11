# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["EmployeeApi.csproj", "./"]
RUN dotnet restore "EmployeeApi.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet build "EmployeeApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "EmployeeApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the URL to listen on all interfaces on port 5000
ENV ASPNETCORE_URLS=http://+:5000

# Expose the port the app runs on
EXPOSE 5000

# Set the entry point
ENTRYPOINT ["dotnet", "EmployeeApi.dll"]