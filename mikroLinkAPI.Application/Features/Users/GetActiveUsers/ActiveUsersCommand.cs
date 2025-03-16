using MediatR;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Users.GetActiveUsers
{
    public sealed record ActiveUsersCommand() : IRequest<Result<Dictionary<string, int>>>;
    internal sealed class ActiveUsersHandler(
       IMemoryCache cache,
       ICurrentUserService currentUserService
        ) : IRequestHandler<ActiveUsersCommand, Result<Dictionary<string, int>>>
    {
        public async Task<Result<Dictionary<string, int>>> Handle(ActiveUsersCommand request, CancellationToken cancellationToken)
        {
            cache.TryGetValue($"Firm_{currentUserService.UserCompanyId}", out Dictionary<string, int> onlineUsers);
            return await Task.FromResult(onlineUsers?.ToDictionary());
        }
    }
}
