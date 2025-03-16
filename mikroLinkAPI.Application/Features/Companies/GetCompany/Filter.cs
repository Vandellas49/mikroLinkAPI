namespace mikroLinkAPI.Application.Features.Companies.GetCompany
{
    public sealed record FilterByCompany(Query<string> Name, Query<string> Email, Query<int> IlId);

}
