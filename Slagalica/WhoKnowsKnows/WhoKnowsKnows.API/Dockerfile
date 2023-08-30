#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WhoKnowsKnows/WhoKnowsKnows.API/WhoKnowsKnows.API.csproj", "WhoKnowsKnows/WhoKnowsKnows.API/"]
COPY ["WhoKnowsKnows/WhoKnowsKnows.Common/WhoKnowsKnows.Common.csproj", "WhoKnowsKnows/WhoKnowsKnows.Common/"]
RUN dotnet restore "WhoKnowsKnows/WhoKnowsKnows.API/WhoKnowsKnows.API.csproj"
COPY . .
WORKDIR "/src/WhoKnowsKnows/WhoKnowsKnows.API"
RUN dotnet build "WhoKnowsKnows.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhoKnowsKnows.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhoKnowsKnows.API.dll"]