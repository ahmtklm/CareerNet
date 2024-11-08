using CareerNetJob.BusinessLogic.Dtos;
using FluentValidation;

namespace CareerNetJob.BusinessLogic.Validations
{
    public  class JobCreateDtoValidator : AbstractValidator<JobCreateDto>
    {
        public JobCreateDtoValidator()
        {
            RuleFor(p => p.Position).NotEmpty().WithMessage("İş ilanı pozisyon bilgisi zorunludur.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("İlan açıklama bilgisi zorunludur.");
        }
    }
}
