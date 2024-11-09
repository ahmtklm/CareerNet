using CareerNetJob.BusinessLogic.Dtos;
using FluentValidation;

namespace CareerNetJob.BusinessLogic.Validations
{
    public  class JobCreateDtoValidator : AbstractValidator<JobCreateDto>
    {
        public JobCreateDtoValidator()
        {
            RuleFor(p => p.CompanyId).NotEmpty().WithMessage("İşveren Id alanı zorunludur.");
            RuleFor(p => p.Position).NotEmpty().WithMessage("İş ilanı pozisyon bilgisi zorunludur.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("İlan açıklama bilgisi zorunludur.");
        }
    }
}
