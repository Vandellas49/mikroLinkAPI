using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Infrastructure.Services;

namespace mikroLinkAPI.Infrastructure.Interceptors
{
    public class InventoryInterceptor(IDashboardNotificationService notificationService, IKafkaProducerService producerService) : SaveChangesInterceptor
    {

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            var componentEntries = context.ChangeTracker.Entries<ComponentSerial>().ToList();
            if (componentEntries.Count == 0)
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            List<int> CompanyIds = context.Set<ComponentSerial>().GroupBy(x => x.CompanyId).Where(c => c.Key != null).Select(c => c.Key.Value).ToList();
            foreach (var comp in CompanyIds)
            {
                int updatedCount = context.Set<ComponentSerial>().Where(x => x.CompanyId == comp).Count();
                notificationService.NotifyInventoryUpdated(updatedCount, comp);
            }
            foreach (var item in componentEntries)
            {
                var entity = item.Entity;
                var userid = entity.CreatedBy;
                var user = context.Set<AccountSsom>().FirstOrDefault(x => x.Id == userid);
                var message = new FirmaEvent { Zaman = DateTime.UtcNow, Text = $"{entity.SeriNo} seri numaralı,{entity.ComponentId} parça kodlu malzemeyi {user.UserName} tarafından oluşturuldu", Type = "info" };
                producerService.SendMessageAsync(message,user.CompanyId);
            }
            return base.SavedChangesAsync(eventData, result, cancellationToken);

        }
    }
}
