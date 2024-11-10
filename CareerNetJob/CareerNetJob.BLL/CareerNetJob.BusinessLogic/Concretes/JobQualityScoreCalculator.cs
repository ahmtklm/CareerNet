using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Dtos;

namespace CareerNetJob.BusinessLogic.Concretes
{
    public class JobQualityScoreCalculator : IQualityScoreCalculator
    {
        private readonly IRestrictedWordsService _restrictedWordsService;

        public JobQualityScoreCalculator(IRestrictedWordsService restrictedWordsService)
        {
            _restrictedWordsService = restrictedWordsService;
        }

        public int CalculateScore(JobCreateDto jobCreateDto)
        {
            int score = 0;

            // Çalışma türü belirtilmişse
            if (!string.IsNullOrEmpty(jobCreateDto.EmploymentType))
                score += 1;

            // Ücret bilgisi belirtilmişse
            if (jobCreateDto.Salary.HasValue)
                score += 1;

            // Yan haklar belirtilmişse
            if (!string.IsNullOrEmpty(jobCreateDto.Benefits))
                score += 1;

            // Sakıncalı kelime kontrolü-Yasaklı kelime içermiyorsa 2 puan
            var containsRestrictedWords = _restrictedWordsService.ContainsRestrictedWords(jobCreateDto.Description).Result;
            if (!containsRestrictedWords)
                score += 2;

            return score;
        }
    }
}
