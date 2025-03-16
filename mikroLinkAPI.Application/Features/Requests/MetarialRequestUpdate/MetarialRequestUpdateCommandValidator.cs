using FluentValidation;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using System.Text;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestUpdate
{
    public sealed class MetarialRequestUpdateCommandValidator : AbstractValidator<MetarialRequestUpdateCommand>
    {
        readonly IMetarialRepository metarialRepository;
        readonly ICurrentUserService currentUser;

        public MetarialRequestUpdateCommandValidator(ICurrentUserService currentUser, IMetarialRepository metarialRepository)
        {
            this.currentUser = currentUser;
            this.metarialRepository = metarialRepository;

            RuleFor(p => p.Aciklama).NotNull().WithMessage("Açıklama alanını giriniz").
            MinimumLength(3).
            WithMessage("Açıklama bilgisi en az 3 karakter olmalıdır");
            RuleFor(p => p.WorkOrderNo).NotNull().WithMessage("İş Emri No alanını giriniz").
            MinimumLength(3).
            WithMessage("İş Emri No alanı en az 3 karakter olmalıdır");
            RuleFor(p => p.TeamLeaderId).NotEqual(0).
            WithMessage("Takım Lideri seçilmelidir");
            RuleFor(p => p.CompanyId).Null().
            When(x => x.TalepTip != 0).
            WithMessage("Firma seçilmelidir");
            RuleFor(x => x.Model).Must(model => model != null && model.Any(c => c.Sturdy + c.Defective + c.Scrap > 0)).
            WithMessage("Talep edilen malzemelerden en az bir tane seçmelisiniz");
            RuleFor(p => p.SiteId).Null().
            When(x => x.TalepTip != 1).
            WithMessage("Saha seçilmelidir");
            RuleFor(p => p).CustomAsync(async (malzemeler, context, cancellationToken) =>
            {
                var hatalar = await MalzemeKontrol(malzemeler, cancellationToken);
                if (!string.IsNullOrEmpty(hatalar))
                {
                    context.AddFailure("Hatalar", hatalar);
                }
            });

        }
        private async Task<string> MalzemeKontrol(MetarialRequestUpdateCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var hatalar = new StringBuilder();
            foreach (var item in model)
            {
                var result = await metarialRepository.Where(p =>
                  p.CompanyId == currentUser.UserCompanyId &&
                  p.MaterialType == item.MaterialType &&
                  p.ComponentId == item.ComponentId).
                  Include(p => p.Component).
                  ThenInclude(p => p.RequestedMaterial.Where(p => p.Request.RequestStatu == (int)RequestStatu.StockMan && p.Request.CompanyId == currentUser.UserCompanyId)).
                  ThenInclude(p => p.Request).
                  Select(p => new { p.Defective, p.Sturdy, p.Scrap, talep = p.Component.RequestedMaterial.Where(c=>c.RequestId!=request.Id).Select(c => new { c.Defective, c.Scrap, c.Sturdy }).ToList() }).
                  ToListAsync(cancellationToken);
                if (result.Count > 0)
                {
                    if (item.Defective > result.Sum(c => c.Defective) - result.FirstOrDefault()?.talep?.Sum(p => p.Defective))
                        hatalar.AppendLine($"{item.ComponentId} malzemesi için talep edilen arzalı malzeme depoda bulunmamaktadır");
                    if (item.Sturdy > result.Sum(c => c.Sturdy) - result.FirstOrDefault()?.talep?.Sum(p => p.Sturdy))
                        hatalar.AppendLine($"{item.ComponentId} malzemesi için talep edilen sağlam malzeme depoda bulunmamaktadır");
                    if (item.Scrap > result.Sum(c => c.Scrap) - result.FirstOrDefault()?.talep?.Sum(p => p.Scrap))
                        hatalar.AppendLine($"{item.ComponentId} malzemesi için talep edilen hurda malzeme depoda bulunmamaktadır");
                }
            }
            return hatalar.ToString().Trim();
        }
    }
}
