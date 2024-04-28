namespace CulinaryRecipes.Context;

/// <summary>
/// Represents settings for the database connection.
/// </summary>
public class DbSettings
{
    /// <summary>
    /// Gets the type of the database.
    /// </summary>
    public DbType Type { get; private set; }

    /// <summary>
    /// Gets the connection string for the database.
    /// </summary>
    public string ConnectionString { get; private set; }

    /// <summary>
    /// Gets the initialization settings for the database.
    /// </summary>
    public DbInitSettings Init { get; private set; }
}

/// <summary>
/// Represents initialization settings for the database.
/// </summary>
public class DbInitSettings
{
    /// <summary>
    /// Gets a value indicating whether to add demo data to the database.
    /// </summary>
    public bool AddDemoData { get; private set; }

    /// <summary>
    /// Gets a value indicating whether to add an administrator to the database.
    /// </summary>
    public bool AddAdministrator { get; private set; }

    /// <summary>
    /// Gets the credentials for the administrator user.
    /// </summary>
    public UserCredentials Administrator { get; private set; }
}

/// <summary>
/// Represents user credentials.
/// </summary>
public class UserCredentials
{
    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the password of the user.
    /// </summary>
    public string Password { get; private set; }
}
