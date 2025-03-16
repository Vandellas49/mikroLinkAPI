using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequest
{
    public sealed record class MetarialRequestCommand(int TalepTip, int? SiteId, int? CompanyId, int TeamLeaderId, string WorkOrderNo, string Aciklama, List<MaterialsRequestVM> Model,int? TradingCompany=null) : IRequest<Result<string>>;
}
