using FluentValidation;

namespace mikroLinkAPI.Application.Features.Sites.SiteAdd
{
    public sealed class CompanyAddValidator : AbstractValidator<SiteAddQueryCommand>
    {
        public CompanyAddValidator()
        {
            RuleFor(p => p.SiteName).MinimumLength(5).WithMessage("Saha İsmi minimum 5 karekter uzunlukta olmalıdır").
                         MaximumLength(50).WithMessage("Saha İsmi maximum 50 karekter uzunlukta olmalıdır");
            RuleFor(p => p.PlanId).NotEmpty().WithMessage("PlanId giriniz");
            RuleFor(p => p.SiteId).NotEmpty().WithMessage("SiteId giriniz");
            RuleFor(p => p.Region).NotEmpty().WithMessage("Bölge giriniz");
            RuleFor(p => p.SiteTip).NotEmpty().WithMessage("SiteTip giriniz");
            RuleFor(p => p.KordinatN).NotEmpty().WithMessage("KordinatN giriniz");
            RuleFor(p => p.KordinatE).NotEmpty().WithMessage("KordinatE giriniz");
            RuleFor(p => p.IlId).GreaterThan(0).WithMessage("İl seçiniz");
        }
    }
}
