namespace CareerNetCompany.Application.Shared.EventsQueue
{
    /// <summary>
    /// Event'ların kullanacağı kuyruk isimlerini tutar.
    /// </summary>
    public static class RabbitMqQueue
    {
        public const string CheckCompanyJobRightEventQueue = "check_company_job_right_event_queue";
        public const string CompanyJobRightConfirmedEventQueue = "company_job_right_confirmed_event_queue";
        public const string CompanyJobRightDeniedEventQueue = "company_job_right_denied_event_queue";
        public const string CalculateJobQualityScoreEventQueue = "calculate_job_quality_score_event_queue";
    }
}
