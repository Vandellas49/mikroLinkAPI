namespace mikroLinkAPI.Application.Services
{
    public interface IDashboardNotificationService
    {
        Task NotifyInventoryUpdated(int updatedCount, int firmId);
        Task NotificationAllChange(string message, int firmId);
    }
}
