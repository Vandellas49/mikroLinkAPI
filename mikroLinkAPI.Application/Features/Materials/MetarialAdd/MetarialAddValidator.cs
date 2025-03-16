using FluentValidation;

namespace mikroLinkAPI.Application.Features.Materials.MetarialAdd
{
    public sealed class MetarialAddValidator : AbstractValidator<MetarialAddCommand> {

        public MetarialAddValidator()
        {
            RuleFor(p => p.SeriNo).MinimumLength(5).WithMessage("Seri numarası minimum 5 karekter uzunlukta olmalıdır").
                             MaximumLength(25).WithMessage("Seri numarası maximum 25 karekter uzunlukta olmalıdır");
            RuleFor(p => p.ComponentId).MinimumLength(5).WithMessage("Parça kodu minimum 5 karekter uzunlukta olmalıdır").
                           MaximumLength(25).WithMessage("Parça kodu maximum 25 karekter uzunlukta olmalıdır");
            RuleFor(p => p.GIrsaliyeNo).NotEmpty().WithMessage("Girişirsaliye No boş geçilemez");
            RuleFor(p => p.Sturdy).GreaterThanOrEqualTo(0).WithMessage("sağlam negatif değer girilmez");
            RuleFor(p => p.Scrap).GreaterThanOrEqualTo(0).WithMessage("hurda negatif değer girilmez");
            RuleFor(p => p.Defective).GreaterThanOrEqualTo(0).WithMessage("arzalı negatif değer girilmez");
        }
    }
}
