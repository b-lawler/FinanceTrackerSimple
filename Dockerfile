#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY SimpleFinanceTracker.UI/SimpleFinanceTracker.UI.csproj UI/
COPY SimpleFinanceTracker.Core/SimpleFinanceTracker.Core.csproj Core/

RUN dotnet restore "UI/SimpleFinanceTracker.UI.csproj"
COPY . .
WORKDIR "/src/SimpleFinanceTracker.UI"
RUN dotnet build "SimpleFinanceTracker.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleFinanceTracker.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleFinanceTracker.UI.dll"]