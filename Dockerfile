FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Install dependencies
COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "epoll.dll"]
