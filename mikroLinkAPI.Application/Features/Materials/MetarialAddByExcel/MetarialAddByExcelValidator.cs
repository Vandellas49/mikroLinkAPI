using FluentValidation;

namespace mikroLinkAPI.Application.Features.Materials.MetarialAddByExcel
{
    public sealed class MetarialAddByExcelValidator : AbstractValidator<MetarialAddByExcelCommand>
    {
        public MetarialAddByExcelValidator()
        {
            RuleFor(p => p.File).NotEmpty().When(p => p.File.Length == 0).WithMessage("Lütfen Belgeyi yükleyiniz");
        }
    }
}
