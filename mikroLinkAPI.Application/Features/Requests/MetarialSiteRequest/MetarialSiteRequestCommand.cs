using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialSiteRequest
{
    public sealed record class MetarialSiteRequestCommand(int? SiteId, string WorkOrderNo, string Aciklama, List<RequestMaterialVM> RequestMaterial) : IRequest<Result<string>>;
}
