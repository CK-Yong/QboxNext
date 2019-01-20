# Use Docker image file with .NET Core installed.
FROM microsoft/dotnet:2.2-aspnetcore-runtime

# Set the working directory to /app
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY ./bin/Release/netcoreapp2.1/publish /app

# Expose port 5001.
EXPOSE 5001

# Start Qservice
ENTRYPOINT ["dotnet", "QboxNext.Qservice.dll"]
