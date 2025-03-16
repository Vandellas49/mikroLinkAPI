using FluentValidation;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
namespace mikroLinkAPI.Application.Features.Requests.MetarialSiteRequest
{
    public sealed class MetarialSiteRequestCommandValidator : AbstractValidator<MetarialSiteRequestCommand>
    {
        public MetarialSiteRequestCommandValidator()
        {

            RuleFor(p => p.Aciklama).NotNull().WithMessage("Açıklama alanını giriniz").
            MinimumLength(3).
            WithMessage("Açıklama bilgisi en az 3 karakter olmalıdır");
            RuleFor(p => p.WorkOrderNo).NotNull().WithMessage("İş Emri No alanını giriniz").
            MinimumLength(3).
            WithMessage("İş Emri No alanı en az 3 karakter olmalıdır");
            //RuleFor(p => p.SiteId).LessThan(0).WithMessage("Saha Seçiniz");
            RuleFor(p => p.RequestMaterial).Must(c=>c.Count>0).WithMessage("Malzeme giriniz");
        }
    }
}
