namespace CareerNetJob.BusinessLogic.Abstractions
{
    /// <summary>
    /// Bir metinde yasaklı kelime olup olmadığını Redis üzerinden kontrol eden arayüz
    /// </summary>
    public interface IRestrictedWordsService
    {
        Task<bool> ContainsRestrictedWords(string text);
    }
}
