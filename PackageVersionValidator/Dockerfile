FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PackageVersionValidator/PackageVersionValidator.csproj", "PackageVersionValidator/"]
RUN dotnet restore "PackageVersionValidator/PackageVersionValidator.csproj"
COPY . .
WORKDIR "/src/PackageVersionValidator"
RUN dotnet build "PackageVersionValidator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PackageVersionValidator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PackageVersionValidator.dll"]
