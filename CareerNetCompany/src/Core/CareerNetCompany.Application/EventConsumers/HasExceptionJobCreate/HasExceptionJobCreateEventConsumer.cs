using AutoMapper;
using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Application.Interfaces.Company;
using CareerNetCompany.Application.Shared.Events;
using MassTransit;

namespace CareerNetCompany.Application.EventConsumers.HasExceptionJobCreate
{
    /// <summary>
    /// Elastic Search'e ilan eklenirken bir hata alma durumunda tetiklenecek Event.
    /// Firmanın İlan yayınlama hakkı bir artırılmalıdır.Company'de güncellenir.
    /// </summary>
    public class HasExceptionJobCreateEventConsumer : IConsumer<HasExceptionJobCreateEvent>
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        /// <param name="mapper"></param>
        public HasExceptionJobCreateEventConsumer(ICompanyService companyService,IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<HasExceptionJobCreateEvent> context)
        {
            var companyId = context.Message.CompanyId;

            var company = await _companyService.GetCompanyByIdAsync(companyId);
            if (company == null)
                throw new Exception("Firma Bulunamadı");

            //İlan oluşturulamadığı durumda firmanın talep gelirken 1 azaltıldığı için hata durumunda 1 arttırılıyor.
            company.JobPostingRightCount--;
            var updateCompanyDto = _mapper.Map<CompanyUpdateDto>(company);
            await _companyService.UpdateCompanyAsync(updateCompanyDto);
            await Task.CompletedTask;
        }
    }
}
