using CareerNetJob.BusinessLogic.EventConsumers.ConfirmedJobRight;
using EventShared.Events;
using MassTransit;

namespace CareerNetJob.BusinessLogic.EventConsumers.DeniedJobRight
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olmadığı durumda tetiklenir.
    /// İlan yayınlanmaz.
    /// Exception döner.
    /// </summary>
    public class CompanyJobRightDeniedEventConsumer : IConsumer<CompanyJobRightDeniedEvent>
    {
        public async Task Consume(ConsumeContext<CompanyJobRightDeniedEvent> context)
        {
            var companyId = context.Message.CompanyId;

            await Task.CompletedTask;
        }
    }
}
