FROM microsoft/aspnetcore:2.0.3
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "EamaShop.Catalog.API.dll"]
