using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerNetJob.API.Controllers
{
    /// <summary>
    /// İş İlanları endpointleri barındırır
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobService"></param>
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        /// <summary>
        /// Tüm İlanları search eder
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _jobService.GetAllJobAsync();
            if (jobs.Count == 0) return NotFound("İş İlanı bulunamadı");

            return Ok(jobs);
        }

        /// <summary>
        /// ExpireDate'a göre elastic search'den ilanları search eder.
        /// </summary>
        /// <param name="jobExpireDate"></param>
        /// <returns></returns>
        [HttpGet("GetAllJobsByExpireDate")]
        public async Task<IActionResult> GetAllJobsByExpireDate(string jobExpireDate)
        {
            var jobs = await _jobService.GetAllJobsByExpireDateAsync(jobExpireDate);
            if (jobs.Count == 0) return NotFound("İş İlanı bulunamadı");

            return Ok(jobs);
        }

        /// <summary>
        /// Yeni iş ilanı yayınlar.
        /// </summary>
        /// <param name="jobCreateDto"></param>
        /// <returns></returns>
        [HttpPost("PublishJob")]
        public async Task<IActionResult> PublishJob(JobCreateDto jobCreateDto)
        {
            await _jobService.CreateJobAsync(jobCreateDto);
            return Created();
        }
    }
}
