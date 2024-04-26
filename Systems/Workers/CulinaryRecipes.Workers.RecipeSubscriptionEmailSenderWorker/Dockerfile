#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Workers/CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker/CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker.csproj", "Systems/Workers/CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker/"]
COPY ["Data/CulinaryRecipes.Context/CulinaryRecipes.Context.csproj", "Data/CulinaryRecipes.Context/"]
COPY ["Services/CulinaryRecipes.Services.Settings/CulinaryRecipes.Services.Settings.csproj", "Services/CulinaryRecipes.Services.Settings/"]
COPY ["Shared/CulinaryRecipes.Common/CulinaryRecipes.Common.csproj", "Shared/CulinaryRecipes.Common/"]
COPY ["Data/CulinaryRecipes.Context.Entities/CulinaryRecipes.Context.Entities.csproj", "Data/CulinaryRecipes.Context.Entities/"]
COPY ["Services/CulinaryRecipes.Services.Actions/CulinaryRecipes.Services.Actions.csproj", "Services/CulinaryRecipes.Services.Actions/"]
COPY ["Services/CulinaryRecipes.Services.EmailSender/CulinaryRecipes.Services.EmailSender.csproj", "Services/CulinaryRecipes.Services.EmailSender/"]
COPY ["Services/CulinaryRecipes.Services.RabbitMq/CulinaryRecipes.Services.RabbitMq.csproj", "Services/CulinaryRecipes.Services.RabbitMq/"]
COPY ["Services/CulinaryRecipes.Services.Logger/CulinaryRecipes.Services.Logger.csproj", "Services/CulinaryRecipes.Services.Logger/"]
RUN dotnet restore "./Systems/Workers/CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker/./CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker.csproj"
COPY . .
WORKDIR "/src/Systems/Workers/CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker"
RUN dotnet build "./CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CulinaryRecipes.Workers.RecipeSubscriptionEmailSenderWorker.dll"]