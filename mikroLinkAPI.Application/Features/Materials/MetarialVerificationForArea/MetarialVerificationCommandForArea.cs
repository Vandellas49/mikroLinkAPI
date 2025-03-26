using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Materials.MetarialVerificationForArea
{
    [Transaction]
    public sealed record class MetarialVerificationCommandForArea(List<ComponentSerialVM> Model) : IRequest<Result<string>>;
}
