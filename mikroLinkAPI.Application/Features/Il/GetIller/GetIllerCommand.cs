using MediatR;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.ViewModel;


namespace mikroLinkAPI.Application.Features.Il.GetIller
{
    public sealed record GetIllerCommand() : IRequest<Result<List<IlVM>>>;

}
