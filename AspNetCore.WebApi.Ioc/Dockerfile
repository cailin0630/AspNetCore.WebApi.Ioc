FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["AspNetCore.WebApi.Ioc/AspNetCore.WebApi.Ioc.csproj", "AspNetCore.WebApi.Ioc/"]
RUN dotnet restore "AspNetCore.WebApi.Ioc/AspNetCore.WebApi.Ioc.csproj"
COPY . .
WORKDIR "/src/AspNetCore.WebApi.Ioc"
RUN dotnet build "AspNetCore.WebApi.Ioc.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AspNetCore.WebApi.Ioc.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspNetCore.WebApi.Ioc.dll"]