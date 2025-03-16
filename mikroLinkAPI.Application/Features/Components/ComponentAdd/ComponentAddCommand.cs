using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentAdd
{

    public sealed record ComponentAddCommand(string Id,int MalzemeTuru,string EquipmentDescription): IRequest<Result<string>>;
}
