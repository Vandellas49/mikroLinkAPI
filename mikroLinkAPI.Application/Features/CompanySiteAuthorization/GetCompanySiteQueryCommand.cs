using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanySiteAuthorization
{
    public sealed record GetCompanySiteQueryCommand(int CompanyId) :IRequest<Result<List<SiteVM>>>;

}
