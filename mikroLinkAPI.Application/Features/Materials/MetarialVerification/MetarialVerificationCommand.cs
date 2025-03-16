using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Materials.MetarialVerification
{
    [Transaction]
    public sealed record class MetarialVerificationCommand(List<ComponentSerialVM> Model, int Id, OperationType OperationType) : IRequest<Result<string>>;
}
