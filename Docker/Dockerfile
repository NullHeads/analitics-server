FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["analitics-server/analitics-server.csproj", "analitics-server/"]
RUN dotnet restore "analitics-server/analitics-server.csproj"
COPY . .
WORKDIR "/src/analitics-server"
RUN dotnet build "analitics-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "analitics-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnalyticsServer.dll"]