using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class MysLislem:Entity
    {
        public  string MysLisGuid { get; set; }
        public DateTime MysLisTstmp { get; set; }
        public string MysLisDname { get; set; }
        public  string MysLdbUygKodu { get; set; }
        public  string MysLdbIp { get; set; }
    }
}
