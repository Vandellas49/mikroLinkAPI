namespace mikroLinkAPI.Application.Features.SiteMetarial.GetMetarials
{
    public sealed record  FilterBySiteMetarial(Query<int?> SiteId, Query<string> SeriNo, Query<string> ComponentId, Query<string> GIrsaliyeNo, Query<int> Sturdy, Query<int> Defective, Query<int> Scrap, Query<string> Shelf,Query<int> MaterialType,Query<string> EquipmentDescription, Query<DateTime> StoreDate);
}
