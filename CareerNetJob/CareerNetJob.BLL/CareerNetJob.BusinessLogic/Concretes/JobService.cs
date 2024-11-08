using AutoMapper;
using CareerNetJob.BusinessLogic.Abstractions;
using CareerNetJob.BusinessLogic.Dtos;
using CareerNetJob.DataAccess.Clients.Abstractions;
using CareerNetJob.DataAccess.EntityModels;

namespace CareerNetJob.BusinessLogic.Concretes
{
    public class JobService : IJobService
    {
        private readonly IElasticSearchClientRepo<Job> _elasticSearchClientRepo;
        private readonly IMapper _mapper;
        private readonly string indexName = "jobs";

        public JobService(IElasticSearchClientRepo<Job> elasticSearchClientRepo, IMapper mapper)
        {
            _elasticSearchClientRepo = elasticSearchClientRepo;
            _mapper = mapper;
        }

        public async Task<JobCreateResponseDto> CreateJobAsync(JobCreateDto jobCreateEntity)
        {
            //Job Mapping
            var jobEntity = _mapper.Map<Job>(jobCreateEntity);
            //İlan yayınlama tarihi
            jobEntity.PostedDate = DateTime.Now;
            //İlanın yayında kalma süresi = ilan yayınlama tarihinden 15 gün sonrası
            var expireDate = jobEntity.PostedDate.AddDays(15);
            jobEntity.ExpireDate = new DateTime(expireDate.Year, expireDate.Month, expireDate.Day);

            //ElasticSearch'e kaydeder.
            var result = await _elasticSearchClientRepo.InsertDocumentAsync(jobEntity, indexName);
            if (!result.IsValidResponse) throw new Exception($"{typeof(Job)} elasticSearch'e kaydedilmedi");

            jobEntity.Id = result.Id;

            //Sonucu map ederek döner.
            return _mapper.Map<JobCreateResponseDto>(jobEntity);
        }

        public async Task<List<JobDto>> GetAllJobAsync()
        {
            var documents = await _elasticSearchClientRepo.GetAllDocuments(indexName);
            return _mapper.Map<List<JobDto>>(documents);
        }

        public async Task<List<JobDto>> GetAllJobsByExpireDateAsync(string expireDate)
        {
            var response = await _elasticSearchClientRepo.GetAllDocumenstByExpiredDateAsync(indexName, expireDate);
            return _mapper.Map<List<JobDto>>(response);
        }
    }
}
