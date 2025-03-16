namespace mikroLinkAPI.Application.Features.Materials.MetarialIn
{
    public sealed record  FilterByMetarialIn(
        Query<int> TeamLeaderId,
        Query<int> FromCompanyId,
        Query<int> FromSiteId,
        Query<int> FromTeamLeaderId,
        Query<int> GirisTipi,
        Query<string> ComponentId,
        Query<string> SeriNo,
        Query<string> GIrsaliyeNo,
        Query<int> MaterialType,
        Query<DateTime> SStoreDate,
        Query<DateTime> FStoreDate
        );
}
