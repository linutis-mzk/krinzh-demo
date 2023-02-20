FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publisher
WORKDIR /app
COPY . .
RUN dotnet publish "Krinzh/Krinzh.csproj" -c Release -o /publish


FROM base
WORKDIR /app
COPY --from=publisher /publish .
COPY /Krinzh/Public/ ./Public/


ENTRYPOINT ["dotnet", "Krinzh.dll"]