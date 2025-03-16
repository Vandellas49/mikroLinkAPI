using mikroLinkAPI.Domain.Enums;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarials
{
    public sealed record DynamicParameter(QueryDynamic<QueryType> QueryType);
}
