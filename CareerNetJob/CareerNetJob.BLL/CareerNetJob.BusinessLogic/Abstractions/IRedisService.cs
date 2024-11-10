namespace CareerNetJob.BusinessLogic.Abstractions
{
    /// <summary>
    /// Redis Cache'den yasaklı kelimeleri getiren arayüz
    /// </summary>
    public interface IRedisService
    {
        Task<List<string>> GetRestrictedWordsAsync();
    }
}
