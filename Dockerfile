
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY ["backend/src/AgendaApp.API/AgendaApp.API.csproj", "backend/src/AgendaApp.API/"]
COPY ["backend/src/AgendaApp.Application/AgendaApp.Application.csproj", "backend/src/AgendaApp.Application/"]
COPY ["backend/src/AgendaApp.Domain/AgendaApp.Domain.csproj", "backend/src/AgendaApp.Domain/"]
COPY ["backend/src/AgendaApp.Infrastructure/AgendaApp.Infrastructure.csproj", "backend/src/AgendaApp.Infrastructure/"]

RUN dotnet restore "backend/src/AgendaApp.API/AgendaApp.API.csproj"

COPY . .

RUN dotnet build "backend/src/AgendaApp.API/AgendaApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "backend/src/AgendaApp.API/AgendaApp.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "AgendaApp.API.dll"] 