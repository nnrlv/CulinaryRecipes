using CulinaryRecipes.Workers.UserSubscriptionEmailSenderWorker.Configuration;
using CulinaryRecipes.Workers.UserSubscriptionEmailSenderWorker;
using CulinaryRecipes.Context;
using CulinaryRecipes.Services.Settings;
using CulinaryRecipes.Settings;
using CulinaryRecipes.Services.Logger;

var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger(logSettings);

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.RegisterAppServices();

// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppHealthChecks();

var logger = app.Services.GetRequiredService<IAppLogger>();

logger.Information("Worker has started");

// Start task executor

logger.Information("Try to connect to RabbitMq");

app.Services.GetRequiredService<ITaskExecutor>().Start();

logger.Information("RabbitMq connected");

// Run app
logger.Information("Worker started");

app.Run();

logger.Information("Worker has stopped");