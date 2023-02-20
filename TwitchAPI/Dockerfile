FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publisher
WORKDIR /app
COPY . .
RUN dotnet publish "TwitchAPI/TwitchAPI.csproj" -c Release -o /publish

FROM base
WORKDIR /app
COPY --from=publisher /publish .


ENTRYPOINT ["dotnet", "TwitchAPI.dll"]
