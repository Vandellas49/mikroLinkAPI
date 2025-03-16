using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Infrastructure.Services;
using Newtonsoft.Json;

namespace mikroLinkAPI.WebAPI.Hubs
{
    [Authorize]
    public class DashboardHub(IMemoryCache cache, IUserSessionService sessionService) : Hub
    {
        private readonly IMemoryCache _cache = cache;
        private static readonly object LockObj = new();

        public override async Task OnConnectedAsync()
        {
            var username = Context.User?.FindFirst("UserName")?.Value;
            var firmId = Context.User?.FindFirst("Company")?.Value;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(firmId))
            {
                lock (LockObj)
                {
                    var onlineUsers = _cache.GetOrCreate($"Firm_{firmId}", entry =>
                    {
                        entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                        return new Dictionary<string, int>();
                    });
                    if (onlineUsers != null)
                    {
                        if (!onlineUsers.TryGetValue(username, out int value))
                            onlineUsers.Add(username, 1);
                        else
                            onlineUsers[username]++;

                    }
                    sessionService.UserConnected(username);
                    _cache.Set($"Firm_{firmId}", onlineUsers);
                }
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Firm_{firmId}");
                await Clients.Group($"Firm_{firmId}").SendAsync("ReceiveOnlineUsers", new { Users = GetOnlineUsersByFirm(firmId) });
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var username = Context.User?.FindFirst("UserName")?.Value;
            var firmId = Context.User?.FindFirst("Company")?.Value;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(firmId))
            {
                lock (LockObj)
                {
                    if (_cache.TryGetValue($"Firm_{firmId}", out Dictionary<string, int>? onlineUsers))
                    {
                        if (onlineUsers != null)
                        {
                            if (onlineUsers[username] == 1)
                                onlineUsers.Remove(username);
                            else
                                onlineUsers[username]--;
                        }
                        sessionService.UserDisconnected(username);
                        _cache.Set($"Firm_{firmId}", onlineUsers);
                    }
                }

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Firm_{firmId}");
                await Clients.Group($"Firm_{firmId}").SendAsync("ReceiveOnlineUsers", new { Users = GetOnlineUsersByFirm(firmId) });
            }

            await base.OnDisconnectedAsync(exception);
        }

        private Dictionary<string, int>? GetOnlineUsersByFirm(string firmId)
        {
            _cache.TryGetValue($"Firm_{firmId}", out Dictionary<string, int>? onlineUsers);
            return onlineUsers;
        }
    }
}
