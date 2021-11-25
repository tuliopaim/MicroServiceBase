#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Email/EmailSender.API/EmailSender.API.csproj", "Email/EmailSender.API/"]
COPY ["Email/EmailSender.Templates/EmailSender.Templates.csproj", "Email/EmailSender.Templates/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "Email/EmailSender.API/EmailSender.API.csproj"
COPY . .
WORKDIR "Email/EmailSender.API"
RUN dotnet build "EmailSender.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailSender.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailSender.API.dll"]