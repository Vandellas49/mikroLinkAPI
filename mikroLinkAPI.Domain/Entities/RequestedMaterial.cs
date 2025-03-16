using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class RequestedMaterial : Entity
    {
        public string ComponentId { get; set; }
        public int RequestId { get; set; }
        public int Sturdy { get; set; }
        public int Defective { get; set; }
        public int Scrap { get; set; }
        public int MaterialType { get; set; }
        public Component Component { get; set; }
        public Request Request { get; set; }
    }
}
