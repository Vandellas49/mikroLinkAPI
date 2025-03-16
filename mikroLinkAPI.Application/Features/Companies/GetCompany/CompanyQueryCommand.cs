
using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Companies.GetCompany
{
    public sealed record CompanyQueryCommand(PageSettings Page, FilterByCompany Filters) : IRequest<Result<Inventory<CompanyVM>>>;

}
