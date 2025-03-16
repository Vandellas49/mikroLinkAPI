using FluentValidation;

namespace mikroLinkAPI.Application.Features.Components.ComponentDelete
{
    public sealed class ComponentDeleteValidator : AbstractValidator<ComponentDeleteCommand>
    {
        public ComponentDeleteValidator()
        {
            RuleFor(p => p.Id).MinimumLength(5).WithMessage("Parça kodu minimum 5 karekter uzunlukta olmalıdır").
                               MaximumLength(25).WithMessage("Parça kodu maximum 25 karekter uzunlukta olmalıdır");
        }
    }
}
