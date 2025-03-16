using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using mikroLinkAPI.Domain.Entities;
namespace mikroLinkAPI.Infrastructure.Interceptors
{
    public class ComponentSerialSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return new ValueTask<InterceptionResult<int>>(result);
            var componentEntries = context.ChangeTracker.Entries<ComponentSerial>().ToList();
            if (componentEntries.Count == 0)
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            HandleStateEntities(componentEntries);
            HandleModifiedEntities(componentEntries, context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return new ValueTask<int>(result);
            var componentEntries = context.ChangeTracker.Entries<ComponentSerial>().ToList();
            if (componentEntries.Count == 0)
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            HandleAddedEntities(componentEntries,context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        private static void HandleStateEntities(List<EntityEntry<ComponentSerial>> entries)
        {
            var addedEntities = entries.Where(e => e.State == EntityState.Added).ToList();
            foreach (var entity in addedEntities)
            {
                entity.Entity.PreviousState = EntityState.Added;
            }
        }
        private static void HandleAddedEntities(List<EntityEntry<ComponentSerial>> entries, DbContext context)
        {
            var addedEntities = entries.Where(e => e.Entity.PreviousState == EntityState.Added).ToList();
            foreach (var entity in addedEntities)
            {
                MaterialTrackingAdd(entity.Entity, context);
                StoreIn(entity.Entity, null, context);
            }
            context.SaveChanges();
        }

        private static void HandleModifiedEntities(List<EntityEntry<ComponentSerial>> entries, DbContext context)
        {
            var modifiedEntities = entries.Where(e => e.State == EntityState.Modified).ToList();
            foreach (var entity in modifiedEntities)
            {
                var original = GetOriginalValues(entity);
                MaterialTrackingAdd(entity.Entity, context);

                if (HasSignificantChanges(original, entity.Entity))
                {
                    StoreIn(entity.Entity, original, context);
                    StoreExit(entity.Entity, original, context);
                }
            }
        }

        private static ComponentSerial GetOriginalValues(EntityEntry<ComponentSerial> entity)
        {
            var original = new ComponentSerial();
            var originalValues = entity.OriginalValues.Clone();

            foreach (var property in originalValues.Properties)
            {
                var originalValue = originalValues[property];
                property.PropertyInfo.SetValue(original, originalValue);
            }
            return original;
        }

        private static bool HasSignificantChanges(ComponentSerial original, ComponentSerial current)
        {
            return !(AreEqualIfNotNull(original.CompanyId, current.CompanyId) ||
                   AreEqualIfNotNull(original.TeamLeaderId, current.TeamLeaderId) ||
                   AreEqualIfNotNull(original.SiteId, current.SiteId));
        }

        private static bool AreEqualIfNotNull<T>(T? original, T? current) where T : struct
        {
            return original != null && current != null && original.Equals(current);
        }

        private static void MaterialTrackingAdd(ComponentSerial material, DbContext context)
        {
            context.Add(new MaterialTracking()
            {
                CompanyId = material.CompanyId,
                CreatedBy = material.CreatedBy,
                CreatedDate = material.CreatedDate,
                CserialId = material.Id,
                Defective = material.Defective,
                Scrap = material.Scrap,
                SiteId = material.SiteId,
                State = material.State,
                Sturdy = material.Sturdy,
                TeamLeaderId = material.TeamLeaderId
            });
        }

        private static void StoreIn(ComponentSerial current, ComponentSerial original, DbContext context)
        {
            context.Add(new StoreIn()
            {
                CompanyId = current.CompanyId,
                TeamLeaderId = current.TeamLeaderId,
                SiteId = current.SiteId,
                CreatedBy = current.CreatedBy,
                CreatedDate = current.CreatedDate,
                CserialId = current.Id,
                Defective = current.Defective,
                Sturdy = current.Sturdy,
                EnterType = original == null ? current.State : original.State,
                Scrap = current.Scrap,
                FromCompanyId = original?.CompanyId,
                FromSiteId = original?.SiteId,
                RequestId = current.RequestSiteCompanySerial.Count > 0 ? current.RequestSiteCompanySerial.FirstOrDefault().RequestId : null,
                FromTeamLeaderId = original?.TeamLeaderId
            });
        }

        private static void StoreExit(ComponentSerial current, ComponentSerial original, DbContext context)
        {
            context.Add(new StoreExit()
            {
                CompanyId = original.CompanyId,
                TeamLeaderId = original.TeamLeaderId,
                SiteId = original.SiteId,
                CreatedBy = current.CreatedBy,
                CreatedDate = current.CreatedDate,
                CserialId = current.Id,
                Defective = original.Defective,
                Sturdy = original.Sturdy,
                ExitType = current.State,
                Scrap = original.Scrap,
                CompanyIdExit = current.CompanyId,
                SiteIdExit = current.SiteId,
                RequestId = current.RequestSiteCompanySerial.Count > 0 ? current.RequestSiteCompanySerial.FirstOrDefault().RequestId : null,
                TeamLeaderIdExit = current.TeamLeaderId
            });
        }
    }

}
