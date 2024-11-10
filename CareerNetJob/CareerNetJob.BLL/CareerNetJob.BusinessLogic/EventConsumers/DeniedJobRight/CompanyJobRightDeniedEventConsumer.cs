﻿using CareerNetJob.BusinessLogic.Shared.Events;
using MassTransit;

namespace CareerNetJob.BusinessLogic.EventConsumers.DeniedJobRight
{
    /// <summary>
    /// Firmanın ilan yayınlama hakkı olmadığında tetiklenecek olan consumer
    /// </summary>
    public class CompanyJobRightDeniedEventConsumer : IConsumer<CompanyJobRightDeniedEvent>
    {
        public async Task Consume(ConsumeContext<CompanyJobRightDeniedEvent> context)
        {
            var companyId = context.Message.CompanyId;

            await Task.CompletedTask;
            throw new Exception($"{companyId} Id'li firmanın ilan yayınlama hakkı yoktur.");
        }
    }
}