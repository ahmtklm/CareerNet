using AutoMapper;
using CareerNetJob.BusinessLogic.Dtos;
using CareerNetJob.DataAccess.EntityModels;

namespace CareerNetJob.BusinessLogic.AutoMappings
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Job,JobCreateDto>().ReverseMap();
            CreateMap<Job,JobDto>().ReverseMap();
            CreateMap<Job,JobCreateResponseDto>().ReverseMap();
        }
    }
}
