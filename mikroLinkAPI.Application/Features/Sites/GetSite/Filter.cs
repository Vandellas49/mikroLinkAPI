namespace mikroLinkAPI.Application.Features.Sites.GetSite
{
    public sealed record  FilterBySite(Query<string> PlanId, Query<string> SiteId, Query<string> Region, Query<string> SiteName, Query<string> SiteTip, Query<int> IlId);
}
