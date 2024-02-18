using CulinaryRecipes.Api.Configuration;
using CulinaryRecipes.Services.Logger;
using CulinaryRecipes.Services.Settings;
using CulinaryRecipes.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   
builder.Services.AddRazorPages();

builder.Services.RegisterServices();

builder.AddAppLogger(mainSettings, logSettings);

builder.Services.AddAppSwagger(mainSettings, swaggerSettings);

builder.Services.AddAppAutoMappers();

builder.Services.AddAppValidator();

builder.Services.AddAppHealthChecks();

builder.Services.AddAppCors();

builder.Services.AddAppControllerAndViews();

builder.Services.AddAppVersioning();


var app = builder.Build();


app.UseAppHealthChecks();

app.UseAppCors();

app.UseAppControllerAndViews();

app.UseAppSwagger();

var logger = app.Services.GetRequiredService<IAppLogger>();

logger.Information("The Culinary Recipes API has started");

app.Run();

logger.Information("The Culinary Recipes API has ended");
