using FluentValidation;

namespace mikroLinkAPI.Application.Features.Components.GetComponentByCode
{
    public sealed class ComponentByCodeQueryValidator : AbstractValidator<ComponentByCodeQuery>
    {
        public ComponentByCodeQueryValidator()
        {
            RuleFor(p => p.parcaKodu).NotEmpty().When(p => p.parcaKodu.Length < 3).WithMessage("Lütfen parça kodunu giriniz");
        }
    }

}
