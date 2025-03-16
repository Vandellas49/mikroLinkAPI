namespace mikroLinkAPI.Application.Features.Materials.MetarialExit
{
    public sealed record  FilterByMetarialExit(
        Query<int> TeamLeaderId,
        Query<int> ExitCompanyId,
        Query<int> ExitSiteId,
        Query<int> ExitTeamLeaderId,
        Query<int> CikisTipi,
        Query<string> ComponentId,
        Query<string> SeriNo,
        Query<string> GIrsaliyeNo,
        Query<int> MaterialType,
        Query<DateTime> SStoreDate,
        Query<DateTime> FStoreDate
        );
}
