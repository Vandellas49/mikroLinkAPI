using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanyToCompanyAuthorization
{
    public sealed record GetCompanyToCompanyQueryCommand(int CompanyId) :IRequest<Result<List<CompanyVM>>>;

}
