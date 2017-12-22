FROM microsoft/aspnetcore:2.0.3
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "EamaShop.Identity.API.dll"]
#用于构建identity身份认证微服务的docker镜像文件