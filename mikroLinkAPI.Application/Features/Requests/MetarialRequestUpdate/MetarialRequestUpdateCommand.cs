using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestUpdate
{
    public sealed record class MetarialRequestUpdateCommand(int Id,int TalepTip, int? SiteId, int? CompanyId, int TeamLeaderId, string WorkOrderNo, string Aciklama, List<MaterialsRequestVM> Model) : IRequest<Result<string>>;
}
