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
            try
            {
                var restrictedWordList = await _database.ListRangeAsync(_redisRestrictedWordsKey);

                //Redis'de yasaklı kelime yoksa boş liste döndürür
                if (!restrictedWordList.Any())
                    return [];

                var restrictedWords = restrictedWordList.Select(p=>p.ToString()).ToList();

                return restrictedWords!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
