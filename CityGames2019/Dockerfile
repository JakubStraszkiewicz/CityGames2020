FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 55927
EXPOSE 44307

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CityGames2019/CityGames2019.csproj", "CityGames2019/"]
RUN dotnet restore "CityGames2019/CityGames2019.csproj"
COPY . .
WORKDIR "/src/CityGames2019"
RUN dotnet build "CityGames2019.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CityGames2019.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CityGames2019.dll"]