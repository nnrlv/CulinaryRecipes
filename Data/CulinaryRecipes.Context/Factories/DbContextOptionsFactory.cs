namespace CulinaryRecipes.Context;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Factory for creating DbContextOptions for the MainDbContext.
/// </summary>
public static class DbContextOptionsFactory
{
    private const string migrationProjectPrefix = "CulinaryRecipes.Context.Migrations.";

    // <summary>
    /// Creates DbContextOptions for the MainDbContext.
    /// </summary>
    /// <param name="connStr">(string) The connection string for the database.</param>
    /// <param name="dbType">(DbType) The type of the database (MSSQL or PgSQL).</param>
    /// <param name="detailedLogging">(bool) Whether to enable detailed logging.</param>
    /// <returns>(DbContextOptions) The created DbContextOptions for the MainDbContext.</returns>
    public static DbContextOptions<MainDbContext> Create(string connStr, DbType dbType, bool detailedLogging = false)
    {
        var bldr = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connStr, dbType, detailedLogging).Invoke(bldr);

        return bldr.Options;
    }

    /// <summary>
    /// Configures the DbContextOptionsBuilder based on the database type and connection string.
    /// </summary>
    /// <param name="connStr">(string) The connection string for the database.</param>
    /// <param name="dbType">(DbType) The type of the database (MSSQL or PgSQL).</param>
    /// <param name="detailedLogging">(bool) Whether to enable detailed logging.</param>
    /// <returns>(Action(DbContextOptionsBuilder)) The configured DbContextOptionsBuilder.</returns>
    public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType dbType, bool detailedLogging = false)
    {
        return (bldr) =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    bldr.UseSqlServer(connStr,
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{migrationProjectPrefix}{DbType.MSSQL}")
                    );
                    break;

                case DbType.PgSQL:
                    bldr.UseNpgsql(connStr,
                        opts => opts
                            .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                            .MigrationsHistoryTable("_migrations")
                            .MigrationsAssembly($"{migrationProjectPrefix}{DbType.PgSQL}")
                    );
                    break;
            }

            if (detailedLogging)
            {
                bldr.EnableSensitiveDataLogging();
            }

            // Attention!
            // It possible to use or LazyLoading or NoTracking at one time
            // Together this features don't work

            //bldr.UseLazyLoadingProxies(true);
        };
        //bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
    }
}