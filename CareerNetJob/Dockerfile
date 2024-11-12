FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .

WORKDIR "/source/CareerNetJob.WebAPI/CareerNetJob.API/"
RUN dotnet restore "CareerNetJob.API.csproj"
RUN dotnet build "CareerNetJob.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CareerNetJob.API.csproj" -c Release -o /app/publish


# final stage/image
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CareerNetJob.API.dll"]