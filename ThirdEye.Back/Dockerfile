#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ThirdEye.Back/ThirdEye.Back.csproj", "ThirdEye.Back/"]
RUN dotnet restore "ThirdEye.Back/ThirdEye.Back.csproj"
COPY . .
WORKDIR "/src/ThirdEye.Back"
RUN dotnet build "ThirdEye.Back.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThirdEye.Back.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThirdEye.Back.dll"]