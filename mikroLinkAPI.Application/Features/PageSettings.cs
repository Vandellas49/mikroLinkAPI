
namespace mikroLinkAPI.Application.Features
{
    public sealed record PageSettings(int page, int pageSize, Sorted sorted,bool All=false);
}
