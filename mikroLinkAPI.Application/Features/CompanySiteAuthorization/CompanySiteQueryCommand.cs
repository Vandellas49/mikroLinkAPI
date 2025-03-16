using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanySiteAuthorization
{
    public sealed record CompanySiteQueryCommand(int CompanyId,List<SiteVM> sites) :IRequest<Result<string>>;

}
