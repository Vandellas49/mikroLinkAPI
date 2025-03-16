using FluentValidation;

namespace mikroLinkAPI.Application.Features.Components.ComponentAddByExcel
{
    public sealed class ComponentAddByExcelValidator : AbstractValidator<ComponentAddByExcelCommand>
    {
        public ComponentAddByExcelValidator()
        {
            RuleFor(p => p.File).NotEmpty().When(p => p.File.Length == 0).WithMessage("Lütfen Belgeyi yükleyiniz");
        }
    }
}
