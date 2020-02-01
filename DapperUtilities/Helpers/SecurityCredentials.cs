using System.Configuration;

namespace DapperUtilities.Helpers
{
    internal sealed class SecurityCredentials
    {
        internal static string GetConnectionString() =>
        $"Server={ConfigurationManager.AppSettings["SqlServerName"]};" +
        $"Database={ConfigurationManager.AppSettings["SqlDatabaseName"]};" +
        $"User Id={ConfigurationManager.AppSettings["SqlUserName"]};" +
        $"Password={ConfigurationManager.AppSettings["SqlPassword"]};";
    }
}
