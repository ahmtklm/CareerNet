namespace CareerNetJob.BusinessLogic.Configuration
{
    /// <summary>
    /// Redis Cache appsettings sınıfını karşılar
    /// </summary>
    public class RedisSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string RestrictedWordsKey { get; set; }
    }
}
