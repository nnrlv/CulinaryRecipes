# Culinary Recipes Exchange Service

## Overview

The Culinary Recipes Exchange Service is a platform for sharing culinary experiences and recipes

## Features

- Publishing, viewing, changing and deleting recipes
- Discussion of recipes by writing comments on them
- Subscription to users and recipes
- Email notifications for new recipes and comments

## Tech Stack

- ASP .NET CORE
- Entity Framework (code first)
- Serilog
- Swagger
- AutoMapper
- FluentValidation
- IdentityServer
- PostgreSQL, SQL Server
- RabbitMQ
- Redis
- Docker, Docker Compose

## Getting Started

1. Clone the repository

2. Fill the settings in `env.emailsenderworker`, `env.usersubscriptionemailsenderworker` and `env.recipesubscriptionemailsenderworker` files. You can use the following example:

   ```plaintext
   EMAILSENDER__EMAIL=culinaryrecipesmail@gmail.com
   EMAILSENDER__PASSWORD=xggmejzsyctheqzq
   EMAILSENDER__HOST=smtp.gmail.com
   EMAILSENDER__PORT=25
   ```

3. Navigate to the project folder and run the following commands:

   ```plaintext
   docker-compose build
   ```
   ```plaintext
   docker-compose up
   ```

4. Register using the method

   ```plaintext
   POST /v1/Accounts/
   ```

5. Confirm your email using the method

   ```plaintext
   POST /v1/Accounts/request-email-confirmation
   ```

   Follow the instructions in the email sent to your email address. Put the confirmation token into the method

   ```plaintext
   PUT /v1/Accounts/confirm-email
   ```

All of the application's functionality can be found in Swagger at **http://localhost:10000/docs/index.html**

## TODO
- Build client web app using Blazor
- Unit & Integration testing

## Database model
![Image](https://github.com/nnr1v/CulinaryRecipes/blob/main/culinary-recipes-db.jpg)
