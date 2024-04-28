namespace CulinaryRecipes.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
/// A factory for creating instances of the MainDbContext class at design time.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    /// <summary>
    /// Creates a new instance of the MainDbContext class.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <returns>A new instance of the MainDbContext class.</returns>
    public MainDbContext CreateDbContext(string[] args)
    {
        var provider = (args?[0] ?? $"{DbType.MSSQL}").ToLower();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.context.json"), false)
            .Build();

        var connStr = configuration.GetConnectionString(provider);

        DbType dbType;
        if (provider.Equals($"{DbType.MSSQL}".ToLower()))
            dbType = DbType.MSSQL;
        else if (provider.Equals($"{DbType.PgSQL}".ToLower()))
            dbType = DbType.PgSQL;
        else
            throw new Exception($"Unsupported provider: {provider}");

        var options = DbContextOptionsFactory.Create(connStr, dbType, false);
        var factory = new DbContextFactory(options);
        var context = factory.Create();

        return context;
    }
}