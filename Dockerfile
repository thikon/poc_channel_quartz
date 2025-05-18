# syntax=docker/dockerfile:1.4
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApiPoc.csproj", "./"]
RUN dotnet restore "WebApiPoc.csproj"
COPY . .
RUN dotnet publish "WebApiPoc.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApiPoc.dll"]