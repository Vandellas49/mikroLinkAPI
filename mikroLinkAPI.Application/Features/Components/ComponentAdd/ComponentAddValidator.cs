using FluentValidation;

namespace mikroLinkAPI.Application.Features.Components.ComponentAdd
{
    public sealed class ComponentAddValidator : AbstractValidator<ComponentAddCommand>
    {
        public ComponentAddValidator()
        {
            RuleFor(p => p.Id).MinimumLength(5).WithMessage("Parça kodu minimum 5 karekter uzunlukta olmalıdır").
                               MaximumLength(50).WithMessage("Parça kodu maximum 25 karekter uzunlukta olmalıdır");
            RuleFor(p => p.EquipmentDescription).MinimumLength(5).WithMessage("Ekipman açıklaması minimum 5 karekter uzunlukta olmalıdır")
                                                .MaximumLength(75).WithMessage("Ekipman açıklaması maximum 75 karekter uzunlukta olmalıdır");
            RuleFor(p => p.MalzemeTuru).NotNull().WithMessage("Malzeme Türü boş geçilemez");
        }
    }
}
