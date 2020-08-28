#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y git
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["KeyStore/KeyStore.csproj", "KeyStore/"]
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y git
RUN dotnet restore "KeyStore/KeyStore.csproj"
COPY . .
WORKDIR "/src/KeyStore"
RUN dotnet build "KeyStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y git
RUN dotnet publish "KeyStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y git
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeyStore.dll"]