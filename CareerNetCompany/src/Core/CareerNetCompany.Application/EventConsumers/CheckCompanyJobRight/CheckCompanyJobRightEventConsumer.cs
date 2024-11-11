using AutoMapper;
using CareerNetCompany.Application.Dtos;
using CareerNetCompany.Application.Interfaces.Company;
using EventShared.Events;
using EventShared.EventsQueue;
using MassTransit;

namespace CareerNetCompany.Application.EventConsumers.CheckCompanyJobRight
{
    /// <summary>
    /// Firmanın iş ilanı yayınlama hakkını kontrol etmek için kullanılan consumer.
    /// Firmanın bilgileri çekilerek ilan yayın hakkı sayısına bakılır.
    /// Yayın hakkı varsa 1 azaltılarak güncellenir ve CompanyJobRightConfirmedEvent event'i fırlatılır.
    /// Yayın hakkı yoksa CompanyJobRightDeniedEvent event'i publish edilir.
    /// </summary>
    public class CheckCompanyJobRightEventConsumer : IConsumer<CheckCompanyJobRightEvent>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sendEndpointProvider"></param>
        /// <param name="publishEndpoint"></param>
        /// <param name="companyService"></param>
        /// <param name="mapper"></param>
        public CheckCompanyJobRightEventConsumer(ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint, ICompanyService companyService, IMapper mapper)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
            _companyService = companyService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CheckCompanyJobRightEvent> context)
        {
            var companyId = context.Message.CompanyId;

            //Firma bilgilerini getirir
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            if (company == null)
                throw new Exception("Firma Bulunamadı");

            if (company.JobPostingRightCount > 0)
            {
                //Firmanın ilan hakkı sayısını 1 azaltır
                company.JobPostingRightCount--;
                //İlan hakkı sayısını günceller
                var updateCompanyDto = _mapper.Map<CompanyUpdateDto>(company);
                await _companyService.UpdateCompanyAsync(updateCompanyDto);

                //Firma ilan yayınlayabilir eventini oluşturur
                CompanyJobRightConfirmedEvent confirmedEvent = new()
                {
                    CompanyId = companyId,
                    Description = context.Message.Description,
                    Position = context.Message.Position,
                    Benefits = context.Message.Benefits,
                    EmploymentType = context.Message.EmploymentType,
                    Salary = context.Message.Salary,
                    HasJobRight = true
                };

                // Başarılı yanıt döner
                await context.RespondAsync(confirmedEvent);

                //Eventi Send ediyoruz belirli kuyruğa.
                ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqQueue.CompanyJobRightConfirmedEventQueue}"));

                await sendEndpoint.Send(confirmedEvent);
            }
            else
            {
                //İlan yayınlama hakkı yoksa bu eventi dinleyen consumer'lara gönderir.
                CompanyJobRightDeniedEvent deniedEvent = new() { CompanyId = companyId, HasJobRight = false };

                // Başarılı yanıt döner
                await context.RespondAsync(deniedEvent);

                await _publishEndpoint.Publish(deniedEvent);
            }
        }
    }
}
