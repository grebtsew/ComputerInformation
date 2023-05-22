# Use an official Microsoft .NET Framework SDK image as the base
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the container
COPY . .

# Build the project
RUN msbuild ./ComputerInformation/ComputerInformation/ComputerInformation.sln /p:Configuration=Release

# Copy the build output to the /app folder
RUN xcopy /y /s /e bin\Release\*.* /app

# Set the entry point for the container
ENTRYPOINT ["cmd", "/k"]
