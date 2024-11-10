using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace CareerNetJob.BusinessLogic.Concretes
{
    /// <summary>
    /// IRedisService Concrete sınıfı
    /// </summary>
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;
        private readonly string _redisRestrictedWordsKey;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="redisSettings"></param>
        public RedisService(IOptions<RedisSettings> redisSettings)
        {
            var settings = redisSettings.Value;
            var redisConnection = $"{settings.Host}:{settings.Port}";
            var redis = ConnectionMultiplexer.Connect(redisConnection);
            _database = redis.GetDatabase();
            _redisRestrictedWordsKey = settings.RestrictedWordsKey;
        }

        public async Task<List<string>> GetRestrictedWordsAsync()
        {
            var restrictedWordsJson = await _database.StringGetAsync(_redisRestrictedWordsKey);

            //Redis'de yasaklı kelime yoksa boş liste döndürür
            if (string.IsNullOrEmpty(restrictedWordsJson))
                return [];

            var restrictedWords = JsonSerializer.Deserialize<List<string>>(restrictedWordsJson!);

            return restrictedWords!;
        }
    }
}
