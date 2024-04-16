#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Api/CulinaryRecipes.Api/CulinaryRecipes.Api.csproj", "Systems/Api/CulinaryRecipes.Api/"]
COPY ["Data/CulinaryRecipes.Context.Migrations/CulinaryRecipes.Context.Migrations.PgSQL/CulinaryRecipes.Context.Migrations.PgSQL.csproj", "Data/CulinaryRecipes.Context.Migrations/CulinaryRecipes.Context.Migrations.PgSQL/"]
COPY ["Data/CulinaryRecipes.Context.Entities/CulinaryRecipes.Context.Entities.csproj", "Data/CulinaryRecipes.Context.Entities/"]
COPY ["Data/CulinaryRecipes.Context/CulinaryRecipes.Context.csproj", "Data/CulinaryRecipes.Context/"]
COPY ["Services/CulinaryRecipes.Services.Settings/CulinaryRecipes.Services.Settings.csproj", "Services/CulinaryRecipes.Services.Settings/"]
COPY ["Shared/CulinaryRecipes.Common/CulinaryRecipes.Common.csproj", "Shared/CulinaryRecipes.Common/"]
COPY ["Data/CulinaryRecipes.Context.Seeder/CulinaryRecipes.Context.Seeder.csproj", "Data/CulinaryRecipes.Context.Seeder/"]
COPY ["Services/CulinaryRecipes.Ingredients/CulinaryRecipes.Services.Ingredients.csproj", "Services/CulinaryRecipes.Ingredients/"]
COPY ["Services/CulinaryRecipes.Services.Cache/CulinaryRecipes.Services.Cache.csproj", "Services/CulinaryRecipes.Services.Cache/"]
COPY ["Services/CulinaryRecipes.Services.Actions/CulinaryRecipes.Services.Actions.csproj", "Services/CulinaryRecipes.Services.Actions/"]
COPY ["Services/CulinaryRecipes.Services.EmailSender/CulinaryRecipes.Services.EmailSender.csproj", "Services/CulinaryRecipes.Services.EmailSender/"]
COPY ["Services/CulinaryRecipes.Services.RabbitMq/CulinaryRecipes.Services.RabbitMq.csproj", "Services/CulinaryRecipes.Services.RabbitMq/"]
COPY ["Services/CulinaryRecipes.Services.Categories/CulinaryRecipes.Services.Categories.csproj", "Services/CulinaryRecipes.Services.Categories/"]
COPY ["Services/CulinaryRecipes.Services.Logger/CulinaryRecipes.Services.Logger.csproj", "Services/CulinaryRecipes.Services.Logger/"]
COPY ["Services/CulinaryRecipes.Services.Recipes/CulinaryRecipes.Services.Recipes.csproj", "Services/CulinaryRecipes.Services.Recipes/"]
COPY ["Services/CulinaryRecipes.Services.UserAccounts/CulinaryRecipes.Services.UserAccounts.csproj", "Services/CulinaryRecipes.Services.UserAccounts/"]
COPY ["Systems/Api/CulinaryRecipes.Api.Settings/CulinaryRecipes.Api.Settings.csproj", "Systems/Api/CulinaryRecipes.Api.Settings/"]
RUN dotnet restore "./Systems/Api/CulinaryRecipes.Api/./CulinaryRecipes.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/CulinaryRecipes.Api"
RUN dotnet build "./CulinaryRecipes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CulinaryRecipes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CulinaryRecipes.Api.dll"]