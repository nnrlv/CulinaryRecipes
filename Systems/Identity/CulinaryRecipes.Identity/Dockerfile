#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Identity/CulinaryRecipes.Identity/CulinaryRecipes.Identity.csproj", "Systems/Identity/CulinaryRecipes.Identity/"]
COPY ["Data/CulinaryRecipes.Context/CulinaryRecipes.Context.csproj", "Data/CulinaryRecipes.Context/"]
COPY ["Services/CulinaryRecipes.Services.Settings/CulinaryRecipes.Services.Settings.csproj", "Services/CulinaryRecipes.Services.Settings/"]
COPY ["Shared/CulinaryRecipes.Common/CulinaryRecipes.Common.csproj", "Shared/CulinaryRecipes.Common/"]
COPY ["Data/CulinaryRecipes.Context.Entities/CulinaryRecipes.Context.Entities.csproj", "Data/CulinaryRecipes.Context.Entities/"]
RUN dotnet restore "./Systems/Identity/CulinaryRecipes.Identity/./CulinaryRecipes.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/CulinaryRecipes.Identity"
RUN dotnet build "./CulinaryRecipes.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CulinaryRecipes.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CulinaryRecipes.Identity.dll"]