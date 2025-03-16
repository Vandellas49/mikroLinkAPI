namespace mikroLinkAPI.Application.Features.Materials.GetMetarials
{
    public sealed record  FilterByMetarial(Query<string> SeriNo, Query<string> ComponentId, Query<string> GIrsaliyeNo, Query<int> Sturdy, Query<int> Defective, Query<int> Scrap, Query<string> Shelf,Query<int> MaterialType,Query<string> EquipmentDescription, Query<DateTime> StoreDate);
}
