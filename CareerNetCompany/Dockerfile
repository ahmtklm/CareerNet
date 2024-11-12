FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .

WORKDIR "/source/src/Presentation/CareerNetCompany.API/"
RUN dotnet restore "CareerNetCompany.API.csproj"
RUN dotnet build "CareerNetCompany.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CareerNetCompany.API.csproj" -c Release -o /app/publish


# final stage/image
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CareerNetCompany.API.dll"]