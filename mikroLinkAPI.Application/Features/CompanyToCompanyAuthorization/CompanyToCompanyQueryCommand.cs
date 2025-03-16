using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanyToCompanyAuthorization
{
    public sealed record CompanyToCompanyQueryCommand(int CompanyId,List<CompanyVM> Companies) :IRequest<Result<string>>;

}
