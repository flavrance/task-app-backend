FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# Copy everything
COPY .. .
RUN ls
# Restore as distinct layers
RUN dotnet restore ../TaskApp-Backend.sln
# Build a release
RUN dotnet build ../TaskApp-Backend.sln -c Release -o /out/build

# Publish a release
FROM build AS publish
RUN dotnet publish ../TaskApp-Backend.sln -c Release -o /out/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /out/publish .
ENTRYPOINT ["dotnet", "TaskApp.WorkerService.dll"]