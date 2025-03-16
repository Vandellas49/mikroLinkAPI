using FluentValidation;

namespace mikroLinkAPI.Application.Features.Companies.CompanyAdd
{
    public sealed class CompanyAddValidator : AbstractValidator<CompanyAddQueryCommand>
    {
        public CompanyAddValidator()
        {
            RuleFor(p => p.Name).MinimumLength(5).WithMessage("Firma İsmi minimum 5 karekter uzunlukta olmalıdır").
                         MaximumLength(25).WithMessage("Firma İsmi maximum 50 karekter uzunlukta olmalıdır");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
            RuleFor(p => p.IlId).GreaterThan(0).WithMessage("İl seçiniz");
        }
    }
}
