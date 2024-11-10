using CareerNetJob.BusinessLogic.Abstractions;

namespace CareerNetJob.BusinessLogic.Concretes
{
    public class RestrictedWordsService : IRestrictedWordsService
    {
        private readonly IRedisService _redisService;

        public RestrictedWordsService(IRedisService redisService)
        {
            _redisService = redisService;
        }

        /// <summary>
        /// Yasaklı kelime içeriyorsa true döner.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<bool> ContainsRestrictedWords(string text)
        {
            var restrictedWords = await _redisService.GetRestrictedWordsAsync();
            return restrictedWords.Any(word => text.Contains(word, StringComparison.OrdinalIgnoreCase));
        }
    }
}
