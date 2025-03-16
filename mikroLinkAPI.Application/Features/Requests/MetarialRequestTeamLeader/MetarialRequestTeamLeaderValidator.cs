using FluentValidation;
namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestTeamLeader
{
    public sealed class MetarialRequestTeamLeaderValidator : AbstractValidator<MetarialRequestTeamLeaderCommand>
    {
        public MetarialRequestTeamLeaderValidator()
        {
            RuleFor(p => p.RequestMaterial).Must(c=>c.Count>0).WithMessage("Malzeme giriniz");
        }
    }
}
