using AutoMapper;
using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Dtos;
using CareerNetJob.DataAccess.Clients.Abstractions;
using CareerNetJob.DataAccess.EntityModels;
using Microsoft.Extensions.Configuration;

namespace CareerNetJob.BusinessLogic.Concretes
{
    public class JobService : IJobService
    {
        private readonly IElasticSearchClientRepo<Job> _elasticSearchClientRepo;
        private readonly IMapper _mapper;
        private readonly string _indexName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elasticSearchClientRepo"></param>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        public JobService(IElasticSearchClientRepo<Job> elasticSearchClientRepo, IMapper mapper,IConfiguration configuration)
        {
            _elasticSearchClientRepo = elasticSearchClientRepo;
            _mapper = mapper;
            _indexName  = configuration?.GetSection("ElasticSearchSettings")["IndexName"]!;
        }

        public async Task<JobCreateResponseDto> CreateJobAsync(JobCreateDto jobCreateDto)
        {
            //Job Mapping
            var jobEntity = _mapper.Map<Job>(jobCreateDto);
            //İlan yayınlama tarihi
            jobEntity.PostedDate = DateTime.Now;
            //İlanın yayında kalma süresi = ilan yayınlama tarihinden 15 gün sonrası
            var expireDate = jobEntity.PostedDate.AddDays(15);
            jobEntity.ExpireDate = new DateTime(expireDate.Year, expireDate.Month, expireDate.Day);

            //ElasticSearch'e kaydeder.
            var result = await _elasticSearchClientRepo.InsertDocumentAsync(jobEntity, _indexName);
            if (!result.IsValidResponse) throw new Exception($"{typeof(Job)} elasticSearch'e kaydedilmedi");

            jobEntity.Id = result.Id;

            //Sonucu map ederek döner.
            return _mapper.Map<JobCreateResponseDto>(jobEntity);
        }

        public async Task<List<JobDto>> GetAllJobAsync()
        {
            var documents = await _elasticSearchClientRepo.GetAllDocuments(_indexName);
            return _mapper.Map<List<JobDto>>(documents);
        }

        public async Task<List<JobDto>> GetAllJobsByExpireDateAsync(string expireDate)
        {
            var response = await _elasticSearchClientRepo.GetAllDocumenstByExpiredDateAsync(_indexName, expireDate);
            return _mapper.Map<List<JobDto>>(response);
        }
    }
}
