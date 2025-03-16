using FluentValidation;
namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestComplate
{
    public sealed class MetarialRequestTeamLeaderValidator : AbstractValidator<MetarialRequestComplateCommand>
    {
        public MetarialRequestTeamLeaderValidator()
        {
            RuleFor(p => p.RequestMaterial).Must(c=>c.Count>0).WithMessage("Malzeme giriniz");
        }
    }
}
