#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/ProcessLayer/Audit/Audit.API/Audit.API.csproj", "src/Services/ProcessLayer/Audit/Audit.API/"]
RUN dotnet restore "src/Services/ProcessLayer/Audit/Audit.API/Audit.API.csproj"
COPY . .
WORKDIR "/src/src/Services/ProcessLayer/Audit/Audit.API"
RUN dotnet build "Audit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Audit.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Audit.API.dll"]
