using MediatR;
using mikroLinkAPI.Application.Features.Components.GetComponent;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentExport
{
    public sealed record ComponentExportCommand(FilterByComponent filters) : IRequest<Result<byte[]>>;

}
