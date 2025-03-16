namespace mikroLinkAPI.Application.Features.Requests.GetReceivedRequests
{
    public sealed record FilterByRequestReceived(Query<int> RequestType, Query<string> RequestDestination, Query<string> CreatedBy, Query<string> TeamlLeader, Query<string> WorkOrderNo, Query<string> Aciklama, Query<DateTime> RequestDate);
}