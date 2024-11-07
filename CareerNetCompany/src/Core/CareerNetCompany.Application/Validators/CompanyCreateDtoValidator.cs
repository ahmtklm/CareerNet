using CareerNetCompany.Application.Dtos;
using FluentValidation;

namespace CareerNetCompany.Application.Validators
{
    /// <summary>
    /// CompanyCreateDto için validasyon kurallarını tanımlayan sınıf.
    /// </summary>
    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Firma adı zorunludur.")
                .MaximumLength(100).WithMessage("Firma adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .Matches(@"^\d{10}$").WithMessage("Telefon numarası 10 haneli olmalıdır.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres zorunludur.")
                .MaximumLength(250).WithMessage("Adres en fazla 250 karakter olabilir.");

            RuleFor(x => x.JobPostingRightCount)
                .GreaterThanOrEqualTo(0).WithMessage("İlan hakkı negatif olamaz.");
        }
    }
}
