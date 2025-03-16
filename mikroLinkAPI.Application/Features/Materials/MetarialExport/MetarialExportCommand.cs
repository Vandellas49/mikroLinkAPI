using MediatR;
using mikroLinkAPI.Application.Features.Materials.GetMetarials;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialExport
{
    public sealed record MetarialExportCommand(FilterByMetarial Filters) : IRequest<Result<byte[]>>;

}
