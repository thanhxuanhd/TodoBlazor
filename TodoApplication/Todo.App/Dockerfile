#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:latest as node
WORKDIR /src
COPY ["Todo.App/package.json", "Todo.App/package-lock.json", "/"]
RUN npm install --production
COPY . .
WORKDIR "/src/Todo.App"
RUN npm run buildcss

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Todo.App/Todo.App.csproj", "Todo.App/"]
RUN dotnet restore "Todo.App/Todo.App.csproj"
COPY . .
WORKDIR "/src/Todo.App"
RUN dotnet build "Todo.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.App.dll"]