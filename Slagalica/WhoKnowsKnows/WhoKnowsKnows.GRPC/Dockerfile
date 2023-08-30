#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WhoKnowsKnows/WhoKnowsKnows.GRPC/WhoKnowsKnows.GRPC.csproj", "WhoKnowsKnows/WhoKnowsKnows.GRPC/"]
COPY ["WhoKnowsKnows/WhoKnowsKnows.Common/WhoKnowsKnows.Common.csproj", "WhoKnowsKnows/WhoKnowsKnows.Common/"]
RUN dotnet restore "WhoKnowsKnows/WhoKnowsKnows.GRPC/WhoKnowsKnows.GRPC.csproj"
COPY . .
WORKDIR "/src/WhoKnowsKnows/WhoKnowsKnows.GRPC"
RUN dotnet build "WhoKnowsKnows.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhoKnowsKnows.GRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhoKnowsKnows.GRPC.dll"]