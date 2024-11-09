using CareerNetJob.BusinessLogic.Dtos;

namespace CareerNetJob.BusinessLogic.Abstractions
{
    /// <summary>
    /// İş ilan işlemlerini barındıran arayüz.
    /// </summary>
    public interface IJobService
    {
        Task<List<JobDto>> GetAllJobAsync();
        Task<List<JobDto>> GetAllJobsByExpireDateAsync(string expireDate);
        Task<JobCreateResponseDto> CreateJobAsync(JobCreateDto jobCreateDto);
    }
}
