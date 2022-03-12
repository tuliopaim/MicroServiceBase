#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/MSBase.Auditoria/MSBase.Auditoria.API.csproj", "src/Services/MSBase.Auditoria/"]
COPY ["src/Core/MSBase.Core/MSBase.Core.csproj", "src/Core/MSBase.Core/"]
RUN dotnet restore "src/Services/MSBase.Auditoria/MSBase.Auditoria.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MSBase.Auditoria"
RUN dotnet build "MSBase.Auditoria.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSBase.Auditoria.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSBase.Auditoria.API.dll"]
