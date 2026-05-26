FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/ImmForm.Api/ImmForm.Api.csproj src/ImmForm.Api/
RUN dotnet restore src/ImmForm.Api/ImmForm.Api.csproj

COPY src/ImmForm.Api/ src/ImmForm.Api/
RUN dotnet publish src/ImmForm.Api/ImmForm.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImmForm.Api.dll"]
