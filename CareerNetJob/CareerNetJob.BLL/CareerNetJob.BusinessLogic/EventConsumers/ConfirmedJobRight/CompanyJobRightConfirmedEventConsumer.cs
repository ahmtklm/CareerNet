using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Dtos;
using CareerNetJob.BusinessLogic.Shared.Events;
using MassTransit;

namespace CareerNetJob.BusinessLogic.EventConsumers.ConfirmedJobRight
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olduğunda tetiklenecek olan consumer
    /// </summary>
    public class CompanyJobRightConfirmedEventConsumer : IConsumer<CompanyJobRightConfirmedEvent>
    {
        private readonly IJobService _jobService;
        private readonly IQualityScoreCalculator _qualityScoreCalculator;
        private readonly IRestrictedWordsService _restrictedWordsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobService"></param>
        /// <param name="qualityScoreCalculator"></param>
        /// <param name="restrictedWordsService"></param>
        public CompanyJobRightConfirmedEventConsumer(IJobService jobService, IQualityScoreCalculator qualityScoreCalculator, IRestrictedWordsService restrictedWordsService)
        {
            _jobService = jobService;
            _qualityScoreCalculator = qualityScoreCalculator;
            _restrictedWordsService = restrictedWordsService;
        }

        public async Task Consume(ConsumeContext<CompanyJobRightConfirmedEvent> context)
        {
            var companyId = context.Message.CompanyId;
            var hasJobRight = context.Message.HasJobRight;
            try
            {
                if (!hasJobRight) throw new Exception($"{companyId} GUID li firmanın ilan yayınlama hakkı kalmamıştır.");

                //İş ilan dto modelini doldur
                var jobCreateDto = new JobCreateDto
                {
                    CompanyId = companyId,
                    Position = context.Message.Position,
                    Description = context.Message.Description,
                    Benefits = context.Message.Benefits,
                    EmploymentType = context.Message.EmploymentType,
                    Salary = context.Message.Salary
                };

                //İlan kalite skoru hesaplama kısmı
                jobCreateDto.QualityScore = _qualityScoreCalculator.CalculateScore(jobCreateDto);

                //İlanı Elastic Search'e kaydet.
                var jobPublishResponse = await _jobService.CreateJobAsync(jobCreateDto);
            }
            catch (Exception)
            {
            };
        }
    }
}
