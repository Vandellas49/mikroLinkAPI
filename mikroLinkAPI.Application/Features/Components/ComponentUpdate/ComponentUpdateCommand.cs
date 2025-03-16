using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentUpdate
{
    public sealed record ComponentUpdateCommand(string Id,int MalzemeTuru,string EquipmentDescription): IRequest<Result<string>>;
}
