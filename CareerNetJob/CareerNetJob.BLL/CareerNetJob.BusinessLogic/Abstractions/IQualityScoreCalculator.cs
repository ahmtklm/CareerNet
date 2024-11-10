using CareerNetJob.BusinessLogic.Dtos;

namespace CareerNetJob.BusinessLogic.Abstractions
{
    /// <summary>
    /// Kalite skorunu hesaplayan arayüz
    /// </summary>
    public interface IQualityScoreCalculator
    {
        int CalculateScore(JobCreateDto jobCreateDto);
    }
}
