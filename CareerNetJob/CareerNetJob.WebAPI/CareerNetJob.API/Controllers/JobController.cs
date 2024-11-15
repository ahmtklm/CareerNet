﻿using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Dtos;
using EventShared.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CareerNetJob.API.Controllers
{
    /// <summary>
    /// İş İlanları endpointleri barındırır
    /// </summary>
    public class JobController : CustomBaseController
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IJobService _jobService;
        private readonly IRequestClient<CheckCompanyJobRightEvent> _requestClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobService"></param>
        /// <param name="publishEndpoint"></param>
        /// <param name="requestClient"></param>
        public JobController(IJobService jobService, IPublishEndpoint publishEndpoint, IRequestClient<CheckCompanyJobRightEvent> requestClient)
        {
            _jobService = jobService;
            _publishEndpoint = publishEndpoint;
            _requestClient = requestClient;
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
            //Firmanın İlan hakkı check eden bir event oluşturuluır
            CheckCompanyJobRightEvent jobEvent = new()
            {
                Benefits = jobCreateDto.Benefits,
                CompanyId = jobCreateDto.CompanyId,
                Description = jobCreateDto.Description,
                Position = jobCreateDto.Position,
                Salary = jobCreateDto.Salary,
                EmploymentType = jobCreateDto.EmploymentType
            };

            var response = await _requestClient.GetResponse<CompanyJobRightDeniedEvent, CompanyJobRightConfirmedEvent>(jobEvent);

            if (response.Is<CompanyJobRightDeniedEvent>(out var deniedEvent))
                return Ok($"{deniedEvent.Message.CompanyId} Id'li Firmanın İlan Yayınlama Hakkı Yoktur.");

            else if (response.Is<CompanyJobRightConfirmedEvent>(out var confirmedEvent))
                return Ok($"{confirmedEvent.Message.CompanyId} Id'li firmanın ilan yayınlama işlemi başarıyla tamamlanmıştır.");

            return NoContent();
        }
    }
}
