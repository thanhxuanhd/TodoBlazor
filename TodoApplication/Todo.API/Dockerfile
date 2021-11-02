#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Todo.API/Todo.API.csproj", "Todo.API/"]
COPY ["Todo.Persistence/Todo.Persistence.csproj", "Todo.Persistence/"]
COPY ["Todo.Application/Todo.Application.csproj", "Todo.Application/"]
COPY ["Todo.Core/Todo.Domain.csproj", "Todo.Core/"]
RUN dotnet restore "Todo.API/Todo.API.csproj"
COPY . .
WORKDIR "/src/Todo.API"
RUN dotnet build "Todo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.API.dll"]