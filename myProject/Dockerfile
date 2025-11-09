

# Use .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy only the project folder
COPY ./myProject ./myProject

# Restore and build inside the project directory
WORKDIR /source/myProject
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "myProject.dll"]
