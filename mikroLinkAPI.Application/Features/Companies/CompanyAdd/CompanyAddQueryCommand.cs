using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Companies.CompanyAdd
{
    public sealed record CompanyAddQueryCommand(string Name, int IlId, string Email) : IRequest<Result<string>>;
}
