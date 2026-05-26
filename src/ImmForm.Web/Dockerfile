FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/ImmForm.Web/ImmForm.Web.csproj src/ImmForm.Web/
RUN dotnet restore src/ImmForm.Web/ImmForm.Web.csproj

COPY src/ImmForm.Web/ src/ImmForm.Web/
RUN dotnet publish src/ImmForm.Web/ImmForm.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImmForm.Web.dll"]
