using mikroLinkAPI.Domain.Abstractions;


namespace mikroLinkAPI.Domain.Entities
{
    public sealed class RequestSiteCompanySerial : Entity
    {
        public int RequestId { get; set; }
        public int CserialId { get; set; }
        public ComponentSerial Cserial { get; set; }
        public Request Request { get; set; }
    }
}
