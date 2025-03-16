using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Companies.CompanyUpdate
{
    public sealed record CompanyUpdateQueryCommand(int Id,string Name, int IlId, string Email) : IRequest<Result<string>>;
}
