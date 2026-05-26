FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/ImmForm.Mocks/ImmForm.Mocks.csproj", "src/ImmForm.Mocks/"]
RUN dotnet restore "src/ImmForm.Mocks/ImmForm.Mocks.csproj"

COPY . .
WORKDIR "/src/src/ImmForm.Mocks"
RUN dotnet publish "ImmForm.Mocks.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImmForm.Mocks.dll"]
