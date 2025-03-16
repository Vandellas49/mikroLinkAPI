
namespace mikroLinkAPI.Application.Features
{
    public class Query<T>
    {
        public T value { get; set; }
        public string matchMode { get; set; }
    }
    public class QueryDynamic<T>
    {
        public T value { get; set; }
    }
}
