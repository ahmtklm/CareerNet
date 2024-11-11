using AutoMapper;
using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Domain.Entities;

namespace CareerNetCompany.Application.MappingProfiles
{
    /// <summary>
    /// Company entity'si ile ilgili DTO'lar arasındaki mapping işlemlerini tanımlar.
    /// </summary>
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            // Company entity'sini CompanyDto'ya map et
            CreateMap<Company, CompanyDto>().ReverseMap();

            // Company entity'sini CompanyCreateDto'ya map et
            CreateMap<CompanyCreateDto, Company>().ReverseMap();

            // Company entity'sini CompanyUpdateDto'ya map et
            CreateMap<CompanyUpdateDto, Company>().ReverseMap();

            // CompanyDto entity'sini CompanyUpdateDto'ya map et
            CreateMap<CompanyUpdateDto, CompanyDto>().ReverseMap();
        }
    }
}
