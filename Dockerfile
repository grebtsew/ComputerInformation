# Use an official Microsoft .NET SDK image as the base
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the container
COPY ComputerInformation.csproj .
COPY . .

# Restore the NuGet packages
RUN dotnet restore

# Build the project
RUN dotnet build -c Release --no-restore

# Run tests
RUN dotnet test

# Publish the project
RUN dotnet publish -c Release --no-build -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/runtime:5.0

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the previous stage
COPY --from=build-env /app/out .

# Set the entry point for the container
#ENTRYPOINT ["dotnet", "ComputerInformation.exe"]
