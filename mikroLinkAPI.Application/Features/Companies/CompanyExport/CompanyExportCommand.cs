using MediatR;
using mikroLinkAPI.Application.Features.Companies.GetCompany;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Companies.CompanyExport
{
    public sealed record CompanyExportCommand(FilterByCompany filters) : IRequest<Result<byte[]>>;
}
