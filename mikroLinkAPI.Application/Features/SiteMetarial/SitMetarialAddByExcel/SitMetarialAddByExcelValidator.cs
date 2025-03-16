using FluentValidation;

namespace mikroLinkAPI.Application.Features.Materials.SiteMetarialAddByExcel
{
    public sealed class SitMetarialAddByExcelValidator : AbstractValidator<SiteMetarialAddByExcelCommand>
    {
        public SitMetarialAddByExcelValidator()
        {
            RuleFor(p => p.File).NotEmpty().When(p => p.File.Length == 0).WithMessage("Lütfen Belgeyi yükleyiniz");
        }
    }
}
