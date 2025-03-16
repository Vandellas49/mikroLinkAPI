using Microsoft.AspNetCore.SignalR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.WebAPI.Hubs;
namespace mikroLinkAPI.Infrastructure.Services
{
    public class DashboardNotificationService(IHubContext<DashboardHub> hubContext) : IDashboardNotificationService
    {
        readonly IHubContext<DashboardHub> _hubContext = hubContext;

        public async Task NotifyInventoryUpdated(int updatedCount,int firmId)
        {
            await _hubContext.Clients.Group($"Firm_{firmId}").SendAsync("ReceiveDashboardUpdate", updatedCount);
        }
        public async Task NotificationAllChange(string message, int firmId)
        {
            await _hubContext.Clients.Group($"Firm_{firmId}").SendAsync("NotificationAllChange", message);
        }
    }
}
