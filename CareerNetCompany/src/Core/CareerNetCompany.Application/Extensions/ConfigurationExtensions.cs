using Microsoft.Extensions.Configuration;

namespace CareerNetCompany.Application.Extensions
{
    /// <summary>
    /// IConfiguration için Appsettings bağlantı bilgilerini almak amacıyla uzantı metotları içerir.
    /// </summary>
    public static class ConfigurationExtensions
    {
        //PostgreSql Connection String bilgisini alır
        public static string? GetPostgreSqlConnectionString(this IConfiguration configurationSection) => configurationSection.GetSection("ConnectionStrings:PostgreSQLConnection").Value;
    }
}
