FROM microsoft/aspnetcore:2.0.3 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0.3 AS build
WORKDIR /src
COPY *.sln ./
COPY EamaShop.WebApp.PC/EamaShop.WebApp.PC.csproj EamaShop.WebApp.PC/
RUN dotnet restore
COPY . .
WORKDIR /src/EamaShop.WebApp.PC
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EamaShop.WebApp.PC.dll"]
