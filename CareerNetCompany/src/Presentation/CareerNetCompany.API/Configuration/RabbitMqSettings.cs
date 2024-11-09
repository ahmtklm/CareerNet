namespace CareerNetCompany.API.Configuration
{
    /// <summary>
    /// RabbitMQ ayarlarını tutmak için kullanılan model sınıfı.
    /// </summary>
    public class RabbitMqSettings
    {
        public string? Host { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
