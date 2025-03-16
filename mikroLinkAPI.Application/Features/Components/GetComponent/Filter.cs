namespace mikroLinkAPI.Application.Features.Components.GetComponent
{
    public sealed record  FilterByComponent(Query<string> Id, Query<string> EquipmentDescription, Query<List<int>> MalzemeTuru);
}
