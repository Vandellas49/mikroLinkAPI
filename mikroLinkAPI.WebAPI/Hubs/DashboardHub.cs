using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.ViewModel;
using System.Collections.Concurrent;

namespace mikroLinkAPI.WebAPI.Hubs
{
    [Authorize]
    public class DashboardHub(IMemoryCache cache, IUserSessionService sessionService) : Hub
    {
        private readonly IMemoryCache _cache = cache;

        public override async Task OnConnectedAsync()
        {
            var username = Context.User?.FindFirst("UserName")?.Value;
            var token = Context.GetHttpContext()?.Request.Headers["Authorization"].FirstOrDefault();
            var firmId = Context.User?.FindFirst("Company")?.Value;
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(firmId))
            {
                var onlineUsers = _cache.GetOrCreate($"Firm_{firmId}", _ => new ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>());

                var userConnections = onlineUsers?.GetOrAdd(username, _ => new ConcurrentBag<UserSignalR>());
                if (userConnections != null)
                    lock (userConnections)
                    {
                        var oldConnections = userConnections.Where(c => c.Token != token).ToList();
                        foreach (var conn in oldConnections)
                        {
                            Clients.Client(conn.ConnectionId).SendAsync("DisconnectUser");
                        }

                        userConnections.Add(new UserSignalR
                        {
                            ConnectionId = connectionId,
                            ConnectionDate = DateTime.Now,
                            Token = token
                        });
                    }

                sessionService.UserConnected(username);
                _cache.Set($"Firm_{firmId}", onlineUsers);

                await Groups.AddToGroupAsync(Context.ConnectionId, $"Firm_{firmId}");
                await Clients.Group($"Firm_{firmId}").SendAsync("ReceiveOnlineUsers", new { Users = GetOnlineUsersByFirm(firmId) });
            }

            await base.OnConnectedAsync();
        }

        public async Task SendHeartbeat(string firmId)
        {
            if (_cache.TryGetValue($"Firm_{firmId}", out ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>? onlineUsers))
            {
                var userConnections = onlineUsers?.Values.SelectMany(x => x);
                var user = userConnections?.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
                if (user != null)
                {
                    user.ConnectionDate = DateTime.Now;
                    _cache.Set($"Firm_{firmId}", onlineUsers);
                }
            }
            await Clients.Group($"Firm_{firmId}").SendAsync("ReceiveOnlineUsers", new { Users = GetOnlineUsersByFirm(firmId) });
        }

        public async Task LogOut()
        {
            var username = Context.User?.FindFirst("UserName")?.Value;
            var firmId = Context.User?.FindFirst("Company")?.Value;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(firmId))
            {
                if (_cache.TryGetValue($"Firm_{firmId}", out ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>? onlineUsers))
                {
                    if (onlineUsers != null)
                        onlineUsers.TryRemove(username, out _);
                    _cache.Set($"Firm_{firmId}", onlineUsers);
                }

                await Clients.Group($"Firm_{firmId}").SendAsync("ReceiveOnlineUsers", new { Users = GetOnlineUsersByFirm(firmId) });
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var username = Context.User?.FindFirst("UserName")?.Value;
            var firmId = Context.User?.FindFirst("Company")?.Value;
            string connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(firmId))
            {
                if (_cache.TryGetValue($"Firm_{firmId}", out ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>? onlineUsers))
                {
                    if (onlineUsers != null && onlineUsers.TryGetValue(username, out var connections))
                    {
                        var updatedConnections = connections.Where(c => c.ConnectionId != connectionId).ToList();

                        if (updatedConnections.Count == 0)
                        {
                            onlineUsers.TryRemove(username, out _);
                        }
                        else
                        {
                            var newBag = new ConcurrentBag<UserSignalR>(updatedConnections);
                            onlineUsers[username] = newBag;
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

        private List<string>? GetOnlineUsersByFirm(string firmId)
        {
            if (_cache.TryGetValue($"Firm_{firmId}", out ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>? onlineUsers))
            {
                return onlineUsers?.Keys.ToList();
            }
            return new List<string>();
        }
    }
}
