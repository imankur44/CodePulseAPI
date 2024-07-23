# Use the official .NET image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CodePulse.API.csproj", "./"]
RUN dotnet restore "CodePulse.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CodePulse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodePulse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodePulse.API.dll"]