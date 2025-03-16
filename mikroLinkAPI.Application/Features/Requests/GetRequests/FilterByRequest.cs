namespace mikroLinkAPI.Application.Features.Requests.GetRequests
{
    public sealed record FilterByRequest(Query<int> RequestStatu, Query<int> Id, Query<int> RequestType, Query<string> RequestDestination, Query<string> CreatedBy, Query<string> TeamlLeader, Query<string> WorkOrderNo, Query<string> Aciklama, Query<DateTime> RequestDate);

}