using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.SiteUpdate;
public sealed record SiteUpdateQueryCommand(int Id,string PlanId, string SiteId, string Region, string SiteName, string SiteTip, int? IlId, string KordinatN, string KordinatE) : IRequest<Result<string>>;
