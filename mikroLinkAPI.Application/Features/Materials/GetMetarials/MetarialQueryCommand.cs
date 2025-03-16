using MediatR;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarials
{
    public sealed record MetarialQueryCommand(
        FilterByMetarial filters, 
        PageSettings Page, DynamicParameter dynamicfield) :IRequest<Result<Inventory<ComponentSerialVM>>>;
}
