using MediatR;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Users.GetActiveUsers
{
    public sealed record ActiveUsersCommand() : IRequest<Result<List<string>>>;
    internal sealed class ActiveUsersHandler(
       IMemoryCache cache,
       ICurrentUserService currentUserService
        ) : IRequestHandler<ActiveUsersCommand, Result<List<string>>>
    {
        public async Task<Result<List<string>>> Handle(ActiveUsersCommand request, CancellationToken cancellationToken)
        {
            cache.TryGetValue($"Firm_{currentUserService.UserCompanyId}", out Dictionary<string, HashSet<UserSignalR>> onlineUsers);
            return await Task.FromResult(onlineUsers?.Select(c => c.Key).ToList());
        }
    }
}
